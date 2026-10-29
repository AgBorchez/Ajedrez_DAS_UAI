using BE_BusinessEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DAL_DataAccesLayer
{
    public class DA_XML_GameRecorder
    {
        public void GuardarHistorialXML(int idGame, Stack<BE_MovementRecord> movimientosPila, string rutaArchivo)
        {
            List<BE_MovementRecord> movimientosOrdenados = movimientosPila.Reverse().ToList();

            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("GameHistory",
                    new XAttribute("IdGame", idGame),
                    new XElement("Moves",
                        movimientosOrdenados.Select((m, index) => new XElement("Move",
                            new XAttribute("Turn", index + 1),
                            new XAttribute("From", m.FromSquare),
                            new XAttribute("To", m.ToSquare),
                            new XAttribute("Special", m.Special)
                        ))
                    )
                )
            );

            doc.Save(rutaArchivo);
        }

        public List<BE_MovementRecord> LeerHistorialXML(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
                throw new FileNotFoundException("El archivo de historial no existe.");

            XDocument doc = XDocument.Load(rutaArchivo);

            // Se leen los nodos <Move> y se ordenan por turno para asegurar la cronología
            var movimientos = doc.Descendants("Move")
                .Select(x => new BE_MovementRecord
                (
                    (int)x.Attribute("Turn"),
                    (string)x.Attribute("From"),
                    (string)x.Attribute("To"),
                    ParsearTipoMovimiento(x.Attribute("Special"))
                    )            
                )
                .OrderBy(m => m.Turn)
                .ToList();

            return movimientos;
        }

        private static BE_MovType ParsearTipoMovimiento(XAttribute attr)
        {
            // 1. Si no existe el atributo en el XML, es un movimiento estándar
            if (attr == null || string.IsNullOrWhiteSpace(attr.Value))
            {
                return BE_MovType.Both; // o el valor por defecto que uses
            }

            // 2. Parseo seguro de string a Enum
            if (Enum.TryParse<BE_MovType>(attr.Value, true, out BE_MovType resultado))
            {
                return resultado;
            }

            // 3. Fallback por si hay algún valor desconocido o corrupto en el XML
            return BE_MovType.Both;
        }
    }
}