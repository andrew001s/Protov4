using System.Data.SqlClient;
using System.Data;
using Protov4.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Collections.Generic;
using NuGet.Protocol.Plugins;
using MongoDB.Driver.Core.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Protov4.DAO
{
    public class CarritoDAO : DbConnection
    {
        private readonly ProductoDAO db;
        SqlCommand cmd = new SqlCommand();
        //DbConnection dbsql;
        SqlDataReader leertabla;
        public CarritoDAO(IConfiguration configuration):base(configuration) 
        {
          db = new ProductoDAO(configuration);
            //dbsql=new DbConnection(configuration);
            // Constructor que llama al constructor de la clase base (DbConnection) pasando la configuración.
            // Esto asegura que el objeto de conexión se inicialice correctamente.

        }

        public void InsertarPedidoDetalle(int id_pedido, string id_producto, decimal precio, int cantidad, decimal subtotal)
        {
            using (var connection=GetSqlConnection())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("InsertarPedidoDetalle", connection);
                cmd.Parameters.AddWithValue("@IdPedido", id_pedido);
                cmd.Parameters.AddWithValue("@IdProducto", id_producto);
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Subtotal", subtotal);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }

        public List<CarritoDTO> ObtenerCarrito()
        {
            List<CarritoDTO> list = new List<CarritoDTO>();

            using (var connection = GetSqlConnection())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCarrito", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                List<CarritoDTO> carr = new List<CarritoDTO>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    carr.Add(new CarritoDTO()
                    {
                        id_pedido_detalle = reader.GetInt32(0),
                        id_pedido = reader.GetInt32(1),
                        id_producto = reader.GetString(2),
                        precio = reader.GetDecimal(3),
                        cantidad = reader.GetInt32(4),
                        subtotal_producto = reader.GetDecimal(5),
                    });

                }

                return carr;
            }

        }
  
        public List<PedidoDTO> ObtenerPedidoPorId(int idPedido)
        {
            List<PedidoDTO> pedidos = new List<PedidoDTO>();

            using (var connection=GetSqlConnection())
            {
                SqlCommand command = new SqlCommand("ObtenerPedido", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@idpedido", idPedido);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PedidoDTO pedido = new PedidoDTO();
                        pedido.id_pedido = (int)reader["id_pedido"];
                        pedido.id_cliente =(int) reader["id_cliente"];
                        pedido.pago_total = (decimal)reader["pago_total"];
                        pedido.Ciudad_envio = reader["Ciudad_envio"].ToString();
                        pedido.Calle_principal = reader["Calle_principal"].ToString();
                        pedido.Calle_secundaria = reader["Calle_secundaria"].ToString();
                        pedido.id_tipo_pago = (int)reader["id_tipo_pago"];
                        pedido.id_tipo_estado = (int)reader["id_tipo_estado"];
                        pedido.fecha_pedido = (DateTime)reader["fecha_pedido"];


                        pedidos.Add(pedido);
                    }
                }
            }

            return pedidos;
        }

        public List<CarritoFullDTO> ObtenerCarritoFull(int id)
        {
            List<CarritoFullDTO> list = new List<CarritoFullDTO>();

            List<CarritoFullDTO> listsql = new List<CarritoFullDTO>();

            var listmongo = new List<CarritoFullDTO>();
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCarrito", connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listsql.Add(new CarritoFullDTO()
                    {
                        Precio = reader.GetDecimal(3),
                        cantidad = reader.GetInt32(4),
                        subtotal_producto = reader.GetDecimal(5),
                        id_producto = reader.GetString(2)
                    });
                }
                foreach (var item in listsql)
                {

                    var pro = db.ObtenerSeleccion(item.id_producto.ToString());
                    var items = pro.Select(p => new CarritoFullDTO
                    {
                        id_producto = p.Id.ToString(),
                        Imagen = p.Imagen,
                        existencias=p.Existencia,
                        Nombre_Producto = p.Nombre_Producto,
                        Precio = 0, // Puedes definirlo como 0 por ahora o ajustarlo después según tus necesidades
                        cantidad = 0, // Puedes definirlo como 0 por ahora o ajustarlo después según tus necesidades
                        subtotal_producto = 0 // Puedes definirlo como 0 por ahora o ajustarlo después según tus necesidades
                    }); ;
                    listmongo.AddRange(items);
                }



                foreach (var itemSql in listsql)
                {
                    // Buscar el elemento correspondiente en listmongo basado en algún identificador único, por ejemplo, supongamos que es por el nombre del producto
                    var itemMongo = listmongo.FirstOrDefault(item => item.id_producto == itemSql.id_producto);

                    if (itemMongo != null)
                    {
                        // Combinar las propiedades del elemento de listsql y listmongo en un nuevo objeto CarritoFullDTO
                        var carritoItem = new CarritoFullDTO
                        {
                            id_producto=itemSql.id_producto,
                            Precio = itemSql.Precio,
                            cantidad = itemSql.cantidad,
                            subtotal_producto = itemSql.subtotal_producto,
                            Imagen = itemMongo.Imagen,
                            existencias=itemMongo.existencias,
                            Nombre_Producto = itemMongo.Nombre_Producto
                        };

                        list.Add(carritoItem);
                    }
                }

                return list;
            }
        }
        public void EliminarProductoCarrito(int id,string idproducto)
        {
            var con = GetSqlConnection();
            cmd.Connection = con;
            cmd.CommandText= "EliminarCarrito";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idpedido", id);
            cmd.Parameters.AddWithValue("@idproducto", idproducto);
            con.Open();
            leertabla=cmd.ExecuteReader();
            con.Close();
        }
        public void RegistrarPedido(int id_cliente)
        {

            using (var connection = GetSqlConnection())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("RegistrarPedido", connection);
                cmd.Parameters.AddWithValue("@id_cliente", id_cliente);
                cmd.Parameters.AddWithValue("@pago_total", DBNull.Value);
                cmd.Parameters.AddWithValue("@Ciudad_envio", DBNull.Value);
                cmd.Parameters.AddWithValue("@Calle_principal", DBNull.Value);
                cmd.Parameters.AddWithValue("@Calle_secundaria", DBNull.Value);
                cmd.Parameters.AddWithValue("@id_tipo_pago", DBNull.Value);
                cmd.Parameters.AddWithValue("@id_tipo_estado", DBNull.Value);
                cmd.Parameters.AddWithValue("@fecha_pedido", DBNull.Value);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
            
        }
        public void ActualizarPedidoDetalle(string id_producto,int id_pedido,int cantidad, decimal subtotal_producto)
        {
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("ActualizarPedidoDetalle", connection);
                cmd.Parameters.AddWithValue("@id_producto", id_producto);
                cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.Parameters.AddWithValue("@subtotal_producto", subtotal_producto);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
       public int ObtenerIdPedido()
{
    int lastPedidoId = -1;

    using (var connection = GetSqlConnection())
    {
        connection.Open();
        using (var cmd = new SqlCommand("GetLastPedidoId", connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter outputParameter = new SqlParameter();
            outputParameter.ParameterName = "@LastPedidoId";
            outputParameter.SqlDbType = SqlDbType.Int;
            outputParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outputParameter);

            cmd.ExecuteNonQuery();

            if (outputParameter.Value != DBNull.Value)
            {
                lastPedidoId = (int)outputParameter.Value;
            }
        }
    }

    return lastPedidoId;
}


        public void ActualizarPedido(int id_pedido, decimal pago_total, int tipoestado,string Ciudad_envio, string Calle_principal, string Calle_secundaria, int id_tipo_pago, DateTime fecha_pedido)
        {
            var con = GetSqlConnection();
            cmd.Connection = con;
            cmd.CommandText = "ActualizarPedido";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
            cmd.Parameters.AddWithValue("@pago_total", pago_total);
            cmd.Parameters.AddWithValue("@Ciudad_envio", Ciudad_envio);
            cmd.Parameters.AddWithValue("@Calle_principal", Calle_principal);
            cmd.Parameters.AddWithValue("@Calle_secundaria", Calle_secundaria);
            cmd.Parameters.AddWithValue("@id_tipo_pago", tipoestado);
            cmd.Parameters.AddWithValue("@id_tipo_estado", tipoestado);
            cmd.Parameters.AddWithValue("@fecha_pedido", fecha_pedido);
            con.Open();
            leertabla = cmd.ExecuteReader();
            con.Close();
        }






    }



}

