using SGITO_OBRAS.Entity;
using SGITO_OBRAS.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Service
{
    public class VentaService
    {
        IUnitOfWork unitOfWork;
        public VentaService() : this(new UnitOfWork()) { }
        public VentaService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }


        public List<ModelVentaHome> GetModeloVentaHome(int UsuarioId)
        {
            List<ModelVentaHome> ModeloVentas = new List<ModelVentaHome>();

            var perfil = unitOfWork.UsuarioRepository.Filter(x => x.usuarioId == UsuarioId).Select(x => x.perfil).FirstOrDefault();
            List<Venta> Ventas = new List<Venta>();
            if (perfil.descripcion == "Visitante" || perfil.descripcion == "Administrador")
            {
                Ventas = unitOfWork.VentaRepository.All().ToList();
            }
            else
            {
                Ventas = unitOfWork.VentaRepository.Filter(x => x.usuarioId == UsuarioId).ToList();
            }
            foreach (var venta in Ventas)
            {
                ModelVentaHome VentasHome = new ModelVentaHome();

                VentasHome.venta = venta;

                VentasHome.rutaAdjunto = unitOfWork.AdjuntosRepository.Filter(x => x.ventaId == venta.ventaId).Select(x => x.rutaAdjunto).FirstOrDefault();

                ModeloVentas.Add(VentasHome);
            }

            return ModeloVentas;
        }



        public string InsertMensaje(int valor, string msj, int usuId)
        {
            try
            {
                var existe = unitOfWork.VentaRepository.Find(x => x.ventaId == valor);
                Mensajes mensaje = new Mensajes();
                Venta venta = unitOfWork.VentaRepository.Find(x => x.ventaId == valor);
                if (existe != null)
                {
                    var UsuarioComprador = unitOfWork.UsuarioRepository.Find(x => x.usuarioId == usuId);
                    mensaje.mensaje = msj;
                    mensaje.ventaId = valor;
                    mensaje.Usuario = usuId;
                    unitOfWork.MensajesRepository.Create(mensaje);
                    unitOfWork.MensajesRepository.Save();
                    venta.mensajes = 1;
                    unitOfWork.VentaRepository.Update(venta);
                    unitOfWork.VentaRepository.Save();


                    return "ok";
                }
                else
                {
                    return "Problemas al cargar mensaje, vuelva a intentar.";
                }
            }
            catch (Exception e)
            {
                return "Error no Controlado: " + e.Message + ". Comuniquese con el administrador.";
            }
        }
        public string InsertVenta(VentaModel ventas, int usuId)
        {
            try
            {
                var existe = unitOfWork.VentaRepository.Find(x => x.nombreVenta == ventas.venta.nombreVenta && x.usuarioId == ventas.venta.usuarioId);
                if (existe == null)
                {
                    unitOfWork.VentaRepository.Create(ventas.venta);
                    ventas.venta.usuarioId = usuId;
                    foreach (string tipo in ventas.desplegableTipo)
                    {
                        ventas.venta.tipo = tipo;
                    }
                    foreach (string pro in ventas.provincias)
                    {
                        ventas.venta.provincia = pro;
                    }
                    unitOfWork.VentaRepository.Save();
                    if (!String.IsNullOrEmpty(ventas.strAdjId))
                    {
                        char[] splitchar = { ';' };
                        var idAdj = ventas.strAdjId.Split(splitchar);
                        for (var count = 0; count <= idAdj.Length - 1; count++)
                        {
                            int idInt = Convert.ToInt32(idAdj[count]);
                            Adjuntos Adj = unitOfWork.AdjuntosRepository.Find(x => x.AdjuntosId == idInt);
                            Adj.ventaId = ventas.venta.ventaId;
                            unitOfWork.AdjuntosRepository.Update(Adj);
                            unitOfWork.AdjuntosRepository.Save();
                        }
                    }
                    return "ok";
                }
                else
                {
                    return "Ya existe una Venta Creada con ese nombre";
                }
            }
            catch (Exception e)
            {
                return "Error no Controlado: " + e.Message + ". Comuniquese con el administrador.";
            }
        }


        public string DeleteVenta(int ventaid)
        {
            try
            {
                var existe = unitOfWork.VentaRepository.Find(x => x.ventaId == ventaid);

                if (existe != null)
                {
                    //Elimino todos los adjuntos relacionados con Venta
                    List<int> adjuntosId = unitOfWork.AdjuntosRepository.Filter(x => x.ventaId == ventaid).Select(x => x.AdjuntosId).ToList();
                    foreach (var num in adjuntosId)
                    {
                        unitOfWork.AdjuntosRepository.Delete(x => x.AdjuntosId == num);
                        unitOfWork.Save();
                    }
                    //Elimino todos los mensajes relacionados con Venta

                    List<int> mensajesId = unitOfWork.MensajesRepository.Filter(x => x.ventaId == ventaid).Select(x => x.MensajesId).ToList();
                    foreach (var num in mensajesId)
                    {
                        unitOfWork.MensajesRepository.Delete(x => x.MensajesId == num);
                        unitOfWork.Save();
                    }
                    unitOfWork.VentaRepository.Delete(x => x.ventaId == ventaid);
                    unitOfWork.Save();
                    return "ok";
                }
                else
                {
                    return "No se encuentra el registro a Eliminar";
                }
            }
            catch (Exception e)
            {
                return "Error no Controlado: " + e.Message + ". Comuniquese con el administrador.";
            }
        }
    }
}


