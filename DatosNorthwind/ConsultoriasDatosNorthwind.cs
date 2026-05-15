using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosNorthwind
{
    public static class ConsultoriasDatosNorthwind
    {

        public static DataTable EjecutarScript(int numeroConsulta)
        {
            using (SqlConnection conexion = new SqlConnection(Properties.Settings.Default.ConexionNorthwind))
            {
                string consulta = "";
                switch (numeroConsulta)
                {
                    case 0:
                        consulta = @"SELECT 
                                        c.CategoryName AS 'Categoría', 
                                        SUM(od.Quantity * od.UnitPrice) AS 'Total Generado'
                                    FROM Categories c
                                    JOIN Products p ON c.CategoryID = p.CategoryID
                                    JOIN [Order Details] od ON p.ProductID = od.ProductID
                                    GROUP BY c.CategoryName";
                        break;
                    case 1:
                        consulta = @"SELECT TOP 10 
                                        p.ProductName AS 'Producto', 
                                        SUM(od.Quantity) AS 'Unidades Vendidas'
                                    FROM Products p
                                    JOIN [Order Details] od ON p.ProductID = od.ProductID
                                    GROUP BY p.ProductName
                                    ORDER BY SUM(od.Quantity) DESC";
                        break;
                    case 2:
                        consulta = @"SELECT 
                                            e.FirstName + ' ' + e.LastName AS 'Empleado', 
                                            COUNT(o.OrderID) AS 'Total Pedidos'
                                        FROM Employees e
                                        JOIN Orders o ON e.EmployeeID = o.EmployeeID
                                        GROUP BY e.FirstName, e.LastName
                                        ORDER BY COUNT(o.OrderID) DESC";
                        break;
                    case 3:
                        consulta = @"SELECT TOP 10
                                        ShipCountry AS 'País Destino', 
                                        SUM(Freight) AS 'Total Fletes'
                                    FROM Orders
                                    GROUP BY ShipCountry
                                    ORDER BY SUM(Freight) DESC";
                        break;
                    case 4:
                        consulta = @"SELECT TOP 10 
                                        c.CompanyName AS 'Cliente', 
                                        COUNT(o.OrderID) AS 'Cantidad Compras'
                                    FROM Customers c
                                    JOIN Orders o ON c.CustomerID = o.CustomerID
                                    GROUP BY c.CompanyName
                                    ORDER BY COUNT(o.OrderID) DESC";
                        break; 
                }

                SqlDataAdapter da = new SqlDataAdapter(consulta, conexion);
                DataTable objetoTabla = new DataTable();
                da.Fill(objetoTabla);
                return objetoTabla;
            }
        }

        public static object EjecutarLinq(int numeroConsulta)
        {
            using (DataNorthwindDataContext contexto = new DataNorthwindDataContext())
            {
                switch (numeroConsulta)
                {
                    case 0:
                        return (from c in contexto.Categories
                                join p in contexto.Products on c.CategoryID equals p.CategoryID
                                join od in contexto.Order_Details on p.ProductID equals od.ProductID
                                group od by c.CategoryName into grupo
                                select new
                                {
                                    Categoría = grupo.Key,
                                    Total_Generado = grupo.Sum(x => x.Quantity * x.UnitPrice)
                                }).ToList();
                    case 1:
                        return (from p in contexto.Products
                                join od in contexto.Order_Details on p.ProductID equals od.ProductID
                                group od by p.ProductName into grupo
                                orderby grupo.Sum(x => x.Quantity) descending
                                select new
                                {
                                    Producto = grupo.Key,
                                    Unidades_Vendidas = grupo.Sum(x => x.Quantity)
                                }).Take(10).ToList();
                    case 2:
                        return (from e in contexto.Employees
                                join o in contexto.Orders on e.EmployeeID equals o.EmployeeID
                                group o by new { e.FirstName, e.LastName } into grupo
                                orderby grupo.Count() descending
                                select new
                                {
                                    Empleado = grupo.Key.FirstName + " " + grupo.Key.LastName,
                                    Total_Pedidos = grupo.Count()
                                }).ToList();
                    case 3:
                        return (from o in contexto.Orders
                                group o by o.ShipCountry into grupo
                                orderby grupo.Sum(x => x.Freight) descending
                                select new
                                {
                                    País_Destino = grupo.Key,
                                    Total_Fletes = grupo.Sum(x => x.Freight)
                                }).Take(10).ToList();
                    case 4:
                        return (from c in contexto.Customers
                                join o in contexto.Orders on c.CustomerID equals o.CustomerID
                                group o by c.CompanyName into grupo
                                orderby grupo.Count() descending
                                select new
                                {
                                    Cliente = grupo.Key,
                                    Cantidad_Compras = grupo.Count()
                                }).Take(10).ToList();

                    default: return null;
                }
            }

        }
    }
}
