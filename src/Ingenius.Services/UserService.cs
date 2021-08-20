using Ingenius.Data.Context;
using Ingenius.Data.Repositories;
using Ingenius.Domain;
using System;
using System.Linq;
using System.Text;

namespace Ingenius.Services
{
    public class UserService:IDisposable
    {
        private readonly  UserRepository _userRepository;
        private readonly IngeniusContext _context;
        public UserService()
        {
            _context = new IngeniusContext();
            _userRepository = new UserRepository(_context);
        }

        public MessageNotification Add(User user)
        {
            if (!user.IsValid())
            {
                return Notificator.Notification(user.ValidationResult);
            }

            user.Password = Convert.ToBase64String(Encoding.ASCII.GetBytes(user.Password));

            var add= _userRepository.Add(user);

            if (add == default)
                return Notificator.Notification("Error al registrar en el base de datos !", "Erro", MessageBoxIcon.Error);
            
            return Notificator.Notification("Datos Registrados !", "Exito", MessageBoxIcon.Information);
        }

        public MessageNotification Update(User user)
        {
            if (!user.IsValid()) 
                return Notificator.Notification(user.ValidationResult);
            
            user.Password = Convert.ToBase64String(Encoding.ASCII.GetBytes(user.Password));

            var update= _userRepository.Update(user);

            if (update == default)
                return Notificator.Notification("Error al registrar en el base de datos !", "Erro", MessageBoxIcon.Error);

            

            return Notificator.Notification("Datos Actualizados !", "Exito", MessageBoxIcon.Information);

        }

        public virtual MessageNotification Remover(Guid id)
        {
           var remove= _userRepository.Remove(id);

            if (remove == default)
                return Notificator.Notification("Error al registrar en el base de datos !", "Erro", MessageBoxIcon.Error);


            return Notificator.Notification("Datos Retidado !", "Exito", MessageBoxIcon.Information);

        }


        public virtual User GetByLogin(string login)=>
            _userRepository.GeetAll(u=>u.Login == login).FirstOrDefault();

        public virtual User GetById(Guid id) =>
            _userRepository.GetById(id);

        public void Dispose()
        {
            _context.Dispose();
            _userRepository.Dispose();
        }
    }
}
