using SGITO_OBRAS.Entity;
using SGITO_OBRAS.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGITO_OBRAS.Service
{
    public class UsuarioService
    {
        IUnitOfWork unitOfWork;
        public UsuarioService() : this(new UnitOfWork()) { }
        public UsuarioService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public int DeleteUsuario(int id)
        {
            try
            {
                unitOfWork.UsuarioRepository.Delete(x => x.usuarioId == id);
                unitOfWork.UsuarioRepository.Save();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public string InsertUsuario(Usuario usuario)
        {
            try
            {
                var existe = unitOfWork.UsuarioRepository.Find(x => x.usuarioRed == usuario.usuarioRed && x.dominio == usuario.dominio);
                if (existe == null)
                {
                    unitOfWork.UsuarioRepository.Create(usuario);
                    unitOfWork.UsuarioRepository.Save();
                    return "ok";
                }
                else
                {
                    return "Ya existe un usuario con el mismo UsuarioRed y Dominio.";
                }  
            }
            catch(Exception e)
            {
                return "Error no Controlado: " + e.Message + ". Comuniquese con el administrador.";
            }
        }
        public string UpdateUsuario(Usuario usuario)
        {
            try
            {
                var existe = unitOfWork.UsuarioRepository.Find(x => x.usuarioRed == usuario.usuarioRed && x.dominio == usuario.dominio && x.usuarioId != usuario.usuarioId);
                if (existe == null)
                {
                    unitOfWork.UsuarioRepository.Update(usuario);
                    unitOfWork.UsuarioRepository.Save();
                    return "ok";
                }
                else
                {
                    return "Ya existe un usuario con el mismo UsuarioRed y Dominio.";
                }
            }
            catch (Exception e)
            {
                return "Error no Controlado: " + e.Message + ". Comuniquese con el administrador.";
            }
        }
    }
}
