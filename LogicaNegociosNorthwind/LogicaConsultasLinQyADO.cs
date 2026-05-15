using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatosNorthwind;

namespace LogicaNegociosNorthwind
{
    public static class LogicaConsultasLinQyADO
    {

        //En ésta capa decido qué metodo de consulta hacer dependiendo lo elegido en el ComboBox de la capa de presentación
        //tambien recibo como parámetro el numero de consulta, que lo va a utilizar la capa de consultas para elegir la consulta

        public static object DevolverDatosConsulta(int numeroConsulta, int metodoConsulta)
        {
            if (metodoConsulta == 0)
            {
                return ConsultoriasDatosNorthwind.EjecutarScript(numeroConsulta);
            }
            else
            {
                return ConsultoriasDatosNorthwind.EjecutarLinq(numeroConsulta);
            }
        }
    }
}
