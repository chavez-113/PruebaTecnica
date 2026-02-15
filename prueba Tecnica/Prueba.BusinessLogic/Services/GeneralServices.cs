using Prueba.BusinessLogic.DTO;
using Prueba.DataAccess.Repositories;
using Prueba.Entities.Entities;
using Prueba.Models;

namespace Prueba.BusinessLogic.Services
{
    public class GeneralServices
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly ProductoRepository _productoRepository;
        private readonly OrdenRepository _ordenRepository;
        private readonly DetalleOrdenRepository _detalleOrdenRepository;

        public GeneralServices(
            ClienteRepository clienteRepository,
            ProductoRepository productoRepository,
            OrdenRepository ordenRepository,
            DetalleOrdenRepository detalleOrdenRepository)
        {
            _clienteRepository = clienteRepository;
            _productoRepository = productoRepository;
            _ordenRepository = ordenRepository;
            _detalleOrdenRepository = detalleOrdenRepository;
        }

        #region Cliente

        public ServiceResult ListarClientes()
        {
            var result = new ServiceResult();
            try
            {
                var clientes = _clienteRepository.List();
                
                if (clientes != null && clientes.Any())
                {
                    return result.Ok("Clientes obtenidos exitosamente", clientes);
                }
                else
                {
                    return result.Ok("No hay clientes registrados", new List<ClienteResponseDto>());
                }
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult BuscarCliente(int id)
        {
            var result = new ServiceResult();
            try
            {
                var cliente = _clienteRepository.Find(id);

                if (cliente.code_Status == 1 && cliente.ClienteId.HasValue)
                {
                    var responseData = new ClienteResponseDto
                    {
                        ClienteId = cliente.ClienteId.Value,
                        Nombre = cliente.Nombre,
                        Identidad = cliente.Identidad
                    };

                    return result.Ok("Cliente encontrado", responseData);
                }
                else
                {
                    return result.NotFound("Cliente no encontrado");
                }
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult InsertarCliente(Cliente item)
        {
            var result = new ServiceResult();
            try
            {
                var insert = _clienteRepository.Insert(item);
                
                if (insert.code_Status == 1 && insert.ClienteId.HasValue)
                {
                    var responseData = new ClienteResponseDto
                    {
                        ClienteId = insert.ClienteId.Value,
                        Nombre = insert.Nombre,
                        Identidad = insert.Identidad
                    };
                    
                    return result.Ok(insert.message_Status, responseData);
                }
                else
                {
                    return result.Error(insert.message_Status ?? "Error al crear cliente");
                }
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult ActualizarCliente(long id, Cliente item)
        {
            var result = new ServiceResult();
            try
            {
                item.ClienteId = id;
                
                var update = _clienteRepository.Update(item);
                
                if (update.code_Status == 1 && update.ClienteId.HasValue)
                {
                    var responseData = new ClienteResponseDto
                    {
                        ClienteId = update.ClienteId.Value,
                        Nombre = update.Nombre,
                        Identidad = update.Identidad
                    };
                    
                    return result.Ok(update.message_Status, responseData);
                }
                else
                {
                    if (update.message_Status != null && update.message_Status.Contains("no encontrado"))
                    {
                        return result.NotFound(update.message_Status);
                    }
                    
                    return result.Error(update.message_Status ?? "Error al actualizar cliente");
                }
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
        
        #endregion

        #region Producto
        
        public ServiceResult ListarProductos()
        {
            var result = new ServiceResult();
            try
            {
                var productos = _productoRepository.List();
                
                if (productos != null && productos.Any())
                {
                    return result.Ok("Productos obtenidos exitosamente", productos);
                }
                else
                {
                    return result.Ok("No hay productos registrados", new List<ProductoResponseDto>());
                }
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult BuscarProducto(long id)
        {
            var result = new ServiceResult();
            try
            {
                var producto = _productoRepository.Find(id);
                
                if (producto.code_Status == 1 && producto.ProductoId.HasValue)
                {
                    var responseData = new ProductoResponseDto
                    {
                        ProductoId = producto.ProductoId.Value,
                        Nombre = producto.Nombre,
                        Descripcion = producto.Descripcion,
                        Precio = producto.Precio.Value,
                        Existencia = producto.Existencia.Value
                    };
                    
                    return result.Ok("Producto encontrado", responseData);
                }
                else
                {
                    return result.NotFound("Producto no encontrado");
                }
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult InsertarProducto(Producto item)
        {
            var result = new ServiceResult();
            try
            {
                var insert = _productoRepository.Insert(item);
                
                if (insert.code_Status == 1 && insert.ProductoId.HasValue)
                {
                    var responseData = new ProductoResponseDto
                    {
                        ProductoId = insert.ProductoId.Value,
                        Nombre = insert.Nombre,
                        Descripcion = insert.Descripcion,
                        Precio = insert.Precio.Value,
                        Existencia = insert.Existencia.Value
                    };
                    
                    return result.Ok(insert.message_Status, responseData);
                }
                else
                {
                    return result.Error(insert.message_Status ?? "Error al crear producto");
                }
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult ActualizarProducto(long id, Producto item)
        {
            var result = new ServiceResult();
            try
            {
                item.ProductoId = id;
                
                var update = _productoRepository.Update(item);
                
                if (update.code_Status == 1 && update.ProductoId.HasValue)
                {
                    var responseData = new ProductoResponseDto
                    {
                        ProductoId = update.ProductoId.Value,
                        Nombre = update.Nombre,
                        Descripcion = update.Descripcion,
                        Precio = update.Precio.Value,
                        Existencia = update.Existencia.Value
                    };
                    
                    return result.Ok(update.message_Status, responseData);
                }
                else
                {
                    if (update.message_Status != null && update.message_Status.Contains("no encontrado"))
                    {
                        return result.NotFound(update.message_Status);
                    }
                    
                    return result.Error(update.message_Status ?? "Error al actualizar producto");
                }
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        #endregion

        #region Orden

       public ServiceResult CrearOrden(OrdenViewModel ordenViewModel)
{
    var result = new ServiceResult();
    var errores = new List<string>();

    try
    {
        // PASO 1: Validar que el cliente exista
        var cliente = _clienteRepository.Find(ordenViewModel.ClienteId);
        if (cliente.code_Status != 1)
        {
            result.Success = false;
            result.Message = "Error al crear la orden";
            result.Errors.Add("El cliente especificado no existe");
            return result;
        }

        // PASO 2: Crear la orden con totales en 0
        var ordenCreada = _ordenRepository.CrearOrden(ordenViewModel.ClienteId, 0, 0, 0);
        
        if (ordenCreada.code_Status != 1 || !ordenCreada.OrdenId.HasValue)
        {
            result.Success = false;
            result.Message = "Error al crear la orden";
            result.Errors.Add(ordenCreada.message_Status ?? "Error al crear la orden");
            return result;
        }

        long ordenId = ordenCreada.OrdenId.Value;
        decimal totalImpuesto = 0;
        decimal totalSubtotal = 0;
        decimal totalGeneral = 0;

        // PASO 3: Procesar cada detalle
        foreach (var detalle in ordenViewModel.Detalle)
        {
            // Validar que el producto exista y obtener su precio
            var producto = _productoRepository.Find(detalle.ProductoId);
            
            if (producto.code_Status != 1 || !producto.ProductoId.HasValue)
            {
                errores.Add($"El producto con ID {detalle.ProductoId} no existe");
                continue;
            }

            // Validar existencia suficiente y actualizar
            var actualizacionExistencia = _detalleOrdenRepository.ActualizarExistenciaProducto(
                detalle.ProductoId, 
                detalle.Cantidad);

            if (actualizacionExistencia.code_Status != 1)
            {
                errores.Add(actualizacionExistencia.message_Status);
                continue;
            }

            // Calcular totales del detalle
            decimal precioProducto = producto.Precio.Value;
            decimal subtotalDetalle = detalle.Cantidad * precioProducto;
            decimal impuestoDetalle = subtotalDetalle * 0.15m;
            decimal totalDetalle = subtotalDetalle + impuestoDetalle;

            // Crear el detalle de la orden
            var detalleCreado = _detalleOrdenRepository.CrearDetalle(
                ordenId,
                detalle.ProductoId,
                detalle.Cantidad,
                impuestoDetalle,
                subtotalDetalle,
                totalDetalle);

            if (detalleCreado.code_Status != 1)
            {
                errores.Add($"Error al crear detalle para producto {detalle.ProductoId}");
                continue;
            }

            // Acumular totales
            totalSubtotal += subtotalDetalle;
            totalImpuesto += impuestoDetalle;
            totalGeneral += totalDetalle;
        }

        // Si hubo errores procesando los detalles, devolver error
        if (errores.Any())
        {
            result.Success = false;
            result.Message = "Error al procesar la orden";
            result.Errors = errores;
            return result;
        }

        // PASO 4: Actualizar los totales de la orden
        var actualizacionOrden = _ordenRepository.ActualizarTotales(
            ordenId,
            totalImpuesto,
            totalSubtotal,
            totalGeneral);

        if (actualizacionOrden.code_Status != 1)
        {
            result.Success = false;
            result.Message = "Error al procesar la orden";
            result.Errors.Add("Error al actualizar totales de la orden");
            return result;
        }

        // PASO 5: Obtener la orden completa con detalles
        var ordenCompleta = _ordenRepository.ObtenerOrdenConDetalles(ordenId);

        if (ordenCompleta == null)
        {
            return result.Error("Error al obtener la orden creada");
        }

        return result.Ok("Orden creada exitosamente", ordenCompleta);
    }
    catch (Exception ex)
    {
        result.Success = false;
        result.Message = "Error inesperado al crear la orden";
        result.Errors.Add(ex.Message);
        return result;
    }
}

        #endregion
    }
}