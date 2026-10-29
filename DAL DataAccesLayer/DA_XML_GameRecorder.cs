using Ajedrez_DAS.DAL;
using BE_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;

namespace DAL_DataAccesLayer
{
    public class DA_XML_GameRecorder
    {
        private readonly Access _access = new Access();

        public void SaveGameHistory(BE_GameRecord record)
        {
            List<BE_MovementRecord> movimientosOrdenados = record.History.Reverse().ToList();

            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("GameHistory",
                    new XAttribute("IdGame", record.IdGame),
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

            string xmlString = doc.ToString();

            // Creación de parámetros usando el método de tu clase Access
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                _access.createParameter("@IdGame", record.IdGame),
                _access.createParameter("@MovesHistory", xmlString)
            };

            // Ejecuta el procedimiento almacenado encargado del guardado
            _access.Write("SP_SaveGameHistory", parametros);
        }

        public void LeerHistorialDB(BE_GameRecord record)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                _access.createParameter("@IdGame", record.IdGame)
            };

            // ReadScalar obtiene el valor de la columna XML devuelta por el SP
            object resultado = _access.ReadScalar("SP_GetGameHistory", parametros);

            if (resultado == null || resultado == DBNull.Value)
            {
                record.History = new Stack<BE_MovementRecord>();
                return;
            }

            string xmlContent = resultado.ToString();

            if (string.IsNullOrWhiteSpace(xmlContent)) 
            { 
                record.History = new Stack<BE_MovementRecord>();
                return;
            }

            XDocument doc = XDocument.Parse(xmlContent);

            List<BE_MovementRecord> history = doc.Descendants("Move")
                .Select(x => new BE_MovementRecord(
                    (int)x.Attribute("Turn"),
                    (string)x.Attribute("From"),
                    (string)x.Attribute("To"),
                    ParseMovType(x.Attribute("Special"))
                ))
                .OrderBy(m => m.Turn)
                .ToList();

            record.History = new Stack<BE_MovementRecord>(history);
        }

        private static BE_MovType ParseMovType(XAttribute attr)
        {
            if (attr == null || string.IsNullOrWhiteSpace(attr.Value))
            {
                return BE_MovType.Both;
            }

            if (Enum.TryParse<BE_MovType>(attr.Value, true, out BE_MovType resultado))
            {
                return resultado;
            }

            return BE_MovType.Both;
        }
    }
}