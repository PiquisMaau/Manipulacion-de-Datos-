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
