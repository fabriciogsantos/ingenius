using Ingenius.Data.Context;
using Ingenius.Data.Repositories;
using Ingenius.Domain;
using System;
using System.Linq;

namespace Ingenius.Services
{
    public class ProductService:IDisposable
    {
        private readonly  ProductRepository _productRepository;
        private readonly  InventoryRepository _iventoryRepository;
        private readonly IngeniusContext _context;
        public ProductService()
        {
            _context = new IngeniusContext();
            _productRepository = new ProductRepository(_context);
            _iventoryRepository = new InventoryRepository(_context);
        }

        public MessageNotification Add(Product product)
        {
            if (!product.IsValid())
            {
                return Notificator.Notification(product.ValidationResult);
            }

            var add= _productRepository.Add(product);

            if (add == default)
                return Notificator.Notification("Error al registrar en el base de datos !", "Erro", MessageBoxIcon.Error);
            
            UpdateInventory(product);

            return Notificator.Notification("Datos Registrados !", "Exito", MessageBoxIcon.Information);
        }

        public MessageNotification Update(Product product)
        {
            if (!product.IsValid()) 
                return Notificator.Notification(product.ValidationResult);
            
           var update= _productRepository.Update(product);

            if (update == default)
                return Notificator.Notification("Error al registrar en el base de datos !", "Erro", MessageBoxIcon.Error);

            

            return Notificator.Notification("Datos Actualizados !", "Exito", MessageBoxIcon.Information);

        }

        public virtual MessageNotification Remover(Guid id)
        {
           var remove= _productRepository.Remove(id);

            if (remove == default)
                return Notificator.Notification("Error al registrar en el base de datos !", "Erro", MessageBoxIcon.Error);


            return Notificator.Notification("Datos Retidado !", "Exito", MessageBoxIcon.Information);

        }

        private void UpdateInventory(Product product)
        {
            var iventoryProduct = _iventoryRepository.GeetAll(s => s.Code == product.Code && s.Size == product.Size).FirstOrDefault();

            if (iventoryProduct == null)
            {
                _iventoryRepository.Add(new Inventory { Code = product.Code, Size = product.Size, Amount = product.Amount });
            }
            else
            {
                if (product.Action == Actions.Add)
                    iventoryProduct.Amount += product.Amount;
                else
                    iventoryProduct.Amount -= product.Amount;

                _iventoryRepository.Update(iventoryProduct);
            }

        }

        public void Dispose()
        {
            _context.Dispose();
            _productRepository.Dispose();
            _iventoryRepository.Dispose();
        }
    }
}
