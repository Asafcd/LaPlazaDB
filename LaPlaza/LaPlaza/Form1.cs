using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LaPlaza
{
    //TABLA PROVEEDOR
    public partial class Form1 : Form
    {
        MySqlConnection cn;
        DataSet datos = new DataSet();
        DataSet datosemp = new DataSet();
        DataSet datosprod = new DataSet();
        DataSet datosventa = new DataSet();
        string fecha;
        public static long id;
        string strCommand;        
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //mostrar venta
            datosventa.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM venta";
            MySqlDataAdapter adaptadorventa = new MySqlDataAdapter(strCommand, cn);
            adaptadorventa.Fill(datosventa, "venta");
            //mostrar datos en el DataGrid
            dgvVentas.DataSource = datosventa.Tables["venta"];
            //Venta_Producto
            lblnfolio.Text = id.ToString();


            //mostrar proveedores
            datos.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM proveedor";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datos, "proveedor");
            //mostrar datos en el DataGrid
            dgvProveedor.DataSource = datos.Tables["proveedor"];

            //mostrar empleados
            datosemp.Clear();
            strCommand = "SELECT * FROM empleado";
            MySqlDataAdapter adaptadoremp = new MySqlDataAdapter(strCommand, cn);
            adaptadoremp.Fill(datosemp, "empleado");
            //mostrar datos en el DataGrid
            dgvEmpleado.DataSource = datosemp.Tables["empleado"];

            //mostrar productos
            datosprod.Clear();
            strCommand = "SELECT * FROM producto";
            MySqlDataAdapter adaptadorprod = new MySqlDataAdapter(strCommand, cn);
            adaptadorprod.Fill(datosprod, "producto");
            //mostrar datos en el DataGrid
            dgvProducto.DataSource = datosprod.Tables["producto"];
            //Cargar productos al ComboBox
            cmbProd.DataSource = datos.Tables["proveedor"];
            cmbProd.DisplayMember = "idProveedor";
            cmbProd.ValueMember = "idProveedor";
        }
        private void dgvProveedor_DoubleClick_1(object sender, EventArgs e)
        {
            txtIDproveedor.Enabled = false;
            txtIDproveedor.Text = dgvProveedor[0, dgvProveedor.CurrentCellAddress.Y].Value.ToString();
            txtaEmpresa.Text = dgvProveedor[1, dgvProveedor.CurrentCellAddress.Y].Value.ToString();
            txtaTelefono.Text = dgvProveedor[2, dgvProveedor.CurrentCellAddress.Y].Value.ToString();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            datos.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM proveedor WHERE empresaProveedor LIKE '%" + txtBuscar.Text + "%'";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datos, "proveedor");
            //mostrar datos en el DataGrid
            dgvProveedor.DataSource = datos.Tables["proveedor"];
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;            
            
            try
            {
                cmd.CommandText = "UPDATE proveedor SET empresaProveedor ='" + txtaEmpresa.Text + "', telProveedor = '" + txtaTelefono.Text + "' WHERE idProveedor='" + txtIDproveedor.Text + "'";
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                MessageBox.Show("Debe llenar todos los campos.");
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    MessageBox.Show("El proveedor se ha actualizado con exito.");

                }
            }
                                    
            datos.Clear();
            strCommand = "SELECT * FROM proveedor";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datos, "proveedor");
            //mostrar datos en el DataGrid
            dgvProveedor.DataSource = datos.Tables["proveedor"];
        }

        private void btnCrear_Click_1(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;
            
            try
            {
                cmd.CommandText = "INSERT INTO proveedor (idProveedor, empresaProveedor, telProveedor)VALUES('" + txtNuevoidprov.Text + "', '" + txtcEmpresa.Text + "','" + txtcTelefono.Text + "')";
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                MessageBox.Show("Debe llenar todos los campos.");
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    
                }
            }

            datos.Clear();
            strCommand = "SELECT * FROM proveedor";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datos, "proveedor");
            //mostrar datos en el DataGrid
            dgvProveedor.DataSource = datos.Tables["proveedor"];
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;

            try
            {
                cmd.CommandText = "DELETE FROM proveedor WHERE idProveedor='" + txtIDproveedor.Text + "'";
                cmd.ExecuteNonQuery();

            }
            
            catch (Exception)
            {
                MessageBox.Show("No se puede eliminar un proveedor asociado a un producto.");
                //throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    
                    Form1_Load(null, null);
                }
            }
            datos.Clear();
            strCommand = "SELECT * FROM proveedor";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datos, "proveedor");
            //mostrar datos en el DataGrid
            dgvProveedor.DataSource = datos.Tables["proveedor"];
        }

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // TABLA EMPLEADO
        private void btnBuscaremp_Click(object sender, EventArgs e)
        {
            datosemp.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM empleado WHERE apellidop_emp or apellidom_emp or nombre_emp LIKE '%" + txtBuscaremp.Text + "%'";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datosemp, "empleado");
            //mostrar datos en el DataGrid
            dgvEmpleado.DataSource = datosemp.Tables["empleado"];
        }

        private void btnCrearemp_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;
            if (txtcAP.Text != "" && txtcAM.Text != "" && txtcNombre.Text != "" && txtcTelemp.Text != "" && txtcTipoemp.Text != "")
            {
                try
                {
                    cmd.CommandText = "INSERT INTO empleado (apellidop_emp, apellidom_emp, nombre_emp, tel_emp, tipo_emp) VALUES('" + txtcAP.Text + "','" + txtcAM.Text + "', '" + txtcNombre.Text + "', '" + txtcTelemp.Text + "','" + txtcTipoemp.Text + "')";
                    cmd.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                        MessageBox.Show("El Empleado se ha creado con exito");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos.");
            }
            
            datosemp.Clear();
            strCommand = "SELECT * FROM empleado";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datosemp, "empleado");
            //mostrar datos en el DataGrid
            dgvEmpleado.DataSource = datosemp.Tables["empleado"];
        }

        private void btnActualizaremp_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;
            if (txtcAP.Text != "" && txtcAM.Text != "" && txtcNombre.Text != "" && txtcTelemp.Text != "" && txtcTipoemp.Text != "")
            {
                try
                {
                    cmd.CommandText = "UPDATE empleado SET apellidop_emp = '" + txtaAP.Text + "',  apellidom_emp = '" + txtaAM.Text + "', nombre_emp = '" + txtaNombre.Text + "', tel_emp = '" + txtaTelemp.Text + "', tipo_emp = '" + cbTipoemp.Text + "' WHERE idempleado = '" + txtaIdemp.Text + "'";
                    cmd.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                        MessageBox.Show("El proveedor se ha actualizado con exito.");
                        Form1_Load(null, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos.");
            }
            
            datosemp.Clear();
            strCommand = "SELECT * FROM empleado";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datosemp, "empleado");
            //mostrar datos en el DataGrid
            dgvEmpleado.DataSource = datosemp.Tables["empleado"];
        }

        private void btnEliminaremp_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;

            try
            {
                cmd.CommandText = "DELETE FROM empleado WHERE idempleado ='" + txtaIdemp.Text + "'";
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    MessageBox.Show("El Empleado se ha eliminado con exito.");
                    Form1_Load(null, null);
                }
            }
            datosemp.Clear();
            strCommand = "SELECT * FROM empleado";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datosemp, "empleado");
            //mostrar datos en el DataGrid
            dgvEmpleado.DataSource = datosemp.Tables["empleado"];
        }

        private void dgvEmpleado_DoubleClick(object sender, EventArgs e)
        {
            txtaIdemp.Enabled = false;
            txtaIdemp.Text = dgvEmpleado[0, dgvEmpleado.CurrentCellAddress.Y].Value.ToString();
            txtaAP.Text = dgvEmpleado[1, dgvEmpleado.CurrentCellAddress.Y].Value.ToString();
            txtaAM.Text = dgvEmpleado[2, dgvEmpleado.CurrentCellAddress.Y].Value.ToString();
            txtaNombre.Text = dgvEmpleado[3, dgvEmpleado.CurrentCellAddress.Y].Value.ToString();
            txtaTelemp.Text = dgvEmpleado[4, dgvEmpleado.CurrentCellAddress.Y].Value.ToString();
            cbTipoemp.Text = dgvEmpleado[5, dgvEmpleado.CurrentCellAddress.Y].Value.ToString();
        }

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //TABLA PRODUCTO
        private void btnBuscarprod_Click(object sender, EventArgs e)
        {
            datosprod.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM producto WHERE descripcion_prod or costo_prod or tipo_prod LIKE '%" + txtBuscarprod.Text + "%'";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datosprod, "producto");
            //mostrar datos en el DataGrid
            dgvProducto.DataSource = datosprod.Tables["producto"];
        }

        private void dgvProducto_DoubleClick(object sender, EventArgs e)
        {
            txtIdProv.Enabled = false;
            txtaIdprod.Enabled = false;  
            txtaIdprod.Text = dgvProducto[0, dgvProducto.CurrentCellAddress.Y].Value.ToString();
            txtaNombreprod.Text = dgvProducto[1, dgvProducto.CurrentCellAddress.Y].Value.ToString();
            txtaDescprod.Text = dgvProducto[2, dgvProducto.CurrentCellAddress.Y].Value.ToString();
            txtaCostoprod.Text = dgvProducto[3, dgvProducto.CurrentCellAddress.Y].Value.ToString();
            txtaTipoprod.Text = dgvProducto[4, dgvProducto.CurrentCellAddress.Y].Value.ToString();
            txtIdProv.Text = dgvProducto[5, dgvProducto.CurrentCellAddress.Y].Value.ToString();
            txtaStock.Text = dgvProducto[6, dgvProducto.CurrentCellAddress.Y].Value.ToString();
        }

        private void btnCrearprod_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;
            
            try
            {
                cmd.CommandText = "INSERT INTO producto (nombre_prod, descripcion_prod, costo_prod, tipo_prod, id_proveedor, stock) VALUES('" + txtcNombreprod.Text + "','" + txtcDescprod.Text + "', '" + txtcCosto.Text + "', '" + txtcTipoprod.Text + "', '" + cmbProd.Text + "', '" + txtcStock.Text + "') ";
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                        
                    Form1_Load(null, null);
                }
            }
                                   
            datosprod.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM producto";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datosprod, "producto");
            //mostrar datos en el DataGrid
            dgvProducto.DataSource = datosprod.Tables["producto"];
        }

        private void btnActualizarprod_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;

            try
            {
                cmd.CommandText = "UPDATE producto SET nombre_prod = '" + txtaNombreprod.Text + "', descripcion_prod = '" + txtaDescprod.Text + "', costo_prod = '" + txtaCostoprod.Text + "', tipo_prod = '" + txtaTipoprod.Text + "', stock = '" + txtaStock.Text + "' WHERE idproducto = '" + txtaIdprod.Text + "'";
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                MessageBox.Show("Debe llenar todos los campos.");
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    
                    Form1_Load(null, null);
                }
            }
                        
            datosprod.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM producto WHERE descripcion_prod or costo_prod or tipo_prod LIKE '%" + txtBuscarprod.Text + "%'";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datosprod, "producto");
            //mostrar datos en el DataGrid
            dgvProducto.DataSource = datosprod.Tables["producto"];
        }

        private void btnEliminarprod_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;

            try
            {
                cmd.CommandText = "DELETE FROM producto WHERE idproducto = '" + txtaIdprod.Text + "'";
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    MessageBox.Show("El Producto se ha eliminado con exito.");
                    Form1_Load(null, null);
                }
            }
            datosprod.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM producto";
            MySqlDataAdapter adaptador = new MySqlDataAdapter(strCommand, cn);
            adaptador.Fill(datosprod, "producto");
            //mostrar datos en el DataGrid
            dgvProducto.DataSource = datosprod.Tables["producto"];
        }

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //TABLA VENTAS

        private void btnBuscarven_Click(object sender, EventArgs e)
        {
            datos.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM venta WHERE total_venta or fecha_venta or idventa LIKE '%" + txtfechav.Text + "%'";
            MySqlDataAdapter adaptadorventa = new MySqlDataAdapter(strCommand, cn);
            adaptadorventa.Fill(datos, "venta");
            //mostrar datos en el DataGrid
            dgvVentas.DataSource = datos.Tables["venta"];
        }

        private void btnNventa_Click(object sender, EventArgs e)
        {
            fecha = DateTime.Now.ToString("yyyy-MM-dd"); //Extraer fecha del sistema
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;
            try
            {
                cmd.CommandText = "INSERT INTO venta(fecha_venta) VALUES('" + fecha + "')";
                cmd.ExecuteNonQuery();
                id = cmd.LastInsertedId;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    MessageBox.Show("Venta "+id+" exitosa.");
                    Form1_Load(null, null);
                }
            }
            datosventa.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM venta";
            MySqlDataAdapter adaptadorventa = new MySqlDataAdapter(strCommand, cn);
            adaptadorventa.Fill(datos, "venta");
            //mostrar datos en el DataGrid
            dgvVentas.DataSource = datos.Tables["venta"];
        }

        private void dgvVentas_DoubleClick(object sender, EventArgs e)
        {
            lblnfolio.Text = dgvVentas[0, dgvVentas.CurrentCellAddress.Y].Value.ToString();
        }

        private void btnBuscProdVenta_Click(object sender, EventArgs e)
        {
            datosprod.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM producto WHERE idproducto or id_proveedor or descripcion_prod or nombre_prod or tipo_prod LIKE '%" + txtBuscProdVenta.Text + "%'";
            MySqlDataAdapter adaptadorprod = new MySqlDataAdapter(strCommand, cn);
            adaptadorprod.Fill(datosprod, "producto");
            //mostrar datos en el DataGrid
            dgvProdVenta.DataSource = datosprod.Tables["producto"];
        }

        private void btnAddprod_Click(object sender, EventArgs e)
        {
            
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;
            
            try
            {
                cmd.CommandText = "INSERT INTO venta_producto(id_venta, id_producto, cantidad) VALUES('" + lblnfolio.Text + "', '" + txtAddIdProd.Text + "', '" + nudCantProdVenta.Value + "')";
                cmd.ExecuteNonQuery();
                id = cmd.LastInsertedId;
            }
            catch (Exception)
            {
                MessageBox.Show("Selecciona un producto.");
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                     
                    Form1_Load(null, null);
                }
            }
            
            datosventa.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM venta";
            MySqlDataAdapter adaptadorventa = new MySqlDataAdapter(strCommand, cn);
            adaptadorventa.Fill(datos, "venta");
            //mostrar datos en el DataGrid
            dgvVentas.DataSource = datos.Tables["venta"];
        }

        private void dgvProdVenta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAddIdProd.Text = dgvProducto[0, dgvProducto.CurrentCellAddress.Y].Value.ToString();
            txtAddProdVenta.Text = dgvProducto[1, dgvProducto.CurrentCellAddress.Y].Value.ToString();
            txtAddIdProd.Enabled = false;
            txtAddProdVenta.Enabled = false;
        }

        private void btnEliminarVenta_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cn = conexion.getConexion();
            cmd.Connection = cn;

            try
            {
                cmd.CommandText = "DELETE FROM venta WHERE idventa = '" + lblnfolio.Text + "'";
                cmd.ExecuteNonQuery();
                id = cmd.LastInsertedId;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                    Form1_Load(null, null);
                }
            }
            datosventa.Clear();
            cn = conexion.getConexion();
            strCommand = "SELECT * FROM venta";
            MySqlDataAdapter adaptadorventa = new MySqlDataAdapter(strCommand, cn);
            adaptadorventa.Fill(datos, "venta");
            //mostrar datos en el DataGrid
            dgvVentas.DataSource = datos.Tables["venta"];

        }
    }  
}
