
///////////////////////////////// SCRIPT DE PRODUCTOS /////////////////////////////////


//Script que carga los selects y llena el modal
$(document).ready(function () {
    $('.btn-modificar-producto').on('click', function () {
        var productoId = $(this).data('producto-id');
        var descripcion = $(this).data('producto-descripcion');
        var existencias = $(this).data('producto-existencias');
        var estadoid = $(this).data('producto-estado_id');
        var estadodescripcion = $(this).data('producto-estado_descripcion');
        var categoriaid = $(this).data('producto-categoria_id');
        var categoriadescripcion = $(this).data('producto-categoria_descripcion');
        var precioStr = $(this).data('producto-precio').toString();
        var precio = parseFloat(precioStr);

        $('#id').val(productoId);
        $('#descripcion').val(descripcion);
        $('#existencias').val(existencias);
        $('#precio').val(precio);
        $('#estadoid').val(estadoid);
        $('#categoriaid').val(categoriaid);
        $('#categoria').val(categoriadescripcion);

        $.ajax({
            url: '/Producto/CargarCategorias',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var select = $('#categoria');
                select.empty();

                $.each(data, function (index, category) {

                    var option = $('<option>').text(category.categoria_descripcion).val(category.categoria_descripcion);

                    option.attr('data-categoria-id', category.categoria_id);

                    select.append(option);

                    if (category.categoria_descripcion === categoriadescripcion) {
                        option.prop('selected', true);
                    }
                });
            },
        });

        $.ajax({
            url: '/Producto/CargarEstados',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var select = $('#estado');
                select.empty();

                $.each(data, function (index, estado) {
                    var option = $('<option>').text(estado.estado_descripcion).val(estado.estado_descripcion);

                    option.attr('data-estado-id', estado.estado_id);

                    select.append(option);

                    if (estado.estado_descripcion === estadodescripcion) {
                        option.prop('selected', true);
                    }
                });
            },
        });


    });
    $("#btnNuevoProducto").click(function () {
        $.ajax({
            url: '/Producto/CargarCategorias',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var select = $('#categoria2');
                select.empty();

                $.each(data, function (index, category) {
                    var option = $('<option>')
                        .val(category.categoria_id)
                        .text(category.categoria_descripcion);

                    select.append(option);
                });
            },
        });
    });

});

//Script que edita el producto
$(document).ready(function () {
    $("#btnGuardarProducto").click(function () {
        var producto_id = $("#id").val();
        var producto_descripcion = $("#descripcion").val();
        var producto_existencias = $("#existencias").val();
        var producto_precio = $("#precio").val();

        var select = document.getElementById('categoria');
        var select2 = document.getElementById('estado');
        var selectedCategoryValue = select.value;
        var selectedEstadoValue = select2.value;
        var selectedCategoryId = $('#categoria option:selected').data('categoria-id');
        var selectedEstadoId = $('#estado option:selected').data('estado-id');
        var urlProductos = document.getElementById('urlProductos').value;

        var entidad = {
            producto_id: producto_id,
            producto_descripcion: producto_descripcion,
            producto_existencias: producto_existencias,
            producto_precio: producto_precio,
            categoria_id: selectedCategoryId,
            estado_id: selectedEstadoId
        };

        $.ajax({
            type: "POST",
            url: "/Producto/EditarProductoAPI",
            data: entidad,
            success: function (result) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'PRODUCTO ACTUALIZADO EXITOSAMENTE',
                    showConfirmButton: false,
                    timer: 1500,
                    width: 400
                });
                setTimeout(function () {
                    window.location.href = urlProductos;
                }, 1500);
            },
            error: function (error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Error al actualizar el producto!'
                });
            }
        });

    });


});

//Script que crea el producto
$("#btncreaproducto").click(function () {
    var producto_descripcion = $("#descripcionnuevo").val().trim();;
    var producto_existencias = $("#cantidadnuevo").val().trim();;
    var producto_precio = $("#precionuevo").val().trim();;
    var selectedCategoryId = $('#categoria2 option:selected').val().trim();;
    var urlProductos = document.getElementById('urlProductos').value;

    if (!producto_descripcion || !producto_existencias || !producto_precio || !selectedCategoryId) {
        Swal.fire({
            icon: 'error',
            title: 'Campos incompletos',
            text: 'Por favor completa todos los campos.',
        });
        return;
    }


    var entidadnueva = {
        producto_descripcion: producto_descripcion,
        producto_existencias: producto_existencias,
        producto_precio: producto_precio,
        categoria_id: selectedCategoryId,
        estado_id: 1
    };


    $.ajax({
        type: "POST",
        url: "/Producto/CrearProducto",
        data: entidadnueva,
        success: function (result) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'PRODUCTO CREADO EXITOSAMENTE',
                showConfirmButton: false,
                timer: 1500,
                width: 400
            });
            setTimeout(function () {
                window.location.href = urlProductos;
            }, 1500);
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Error al crear el producto!'
            });
        },
    });

});


//Script que borra el producto
$(document).ready(function () {
    $(".btnborraproducto").click(function () {
        var producto_id = $(this).data("producto-id");
        var urlProductos = document.getElementById('urlProductos').value;
        Swal.fire({
            title: 'Esta seguro que desea borrar el producto?',
            text: "No podras revertir los cambios!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, eliminar!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/Producto/EliminaProducto',
                    data: { producto_id: producto_id },
                    success: function (response) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'PRODUCTO ELIMINADO EXITOSAMENTE',
                            showConfirmButton: false,
                            timer: 1500,
                            width: 400
                        });
                        setTimeout(function () {
                            window.location.href = urlProductos;
                        }, 1500);
                    },
                    error: function (error) {
                        Swal.fire(
                            'Error',
                            'Ocurrió un error al eliminar el producto.',
                            'error'
                        );
                    }
                });
            }
        });
    });
});
//
///////////////////////////////// SCRIPT DE PRODUCTOS FINAL /////////////////////////////////

///////////////////////////////// SCRIPT DE CLIENTES /////////////////////////////////

//Script que carga los datos del producto en el modal
$(document).ready(function () {
    $('.btn-modificar-cliente').on('click', function () {
        var clienteId = $(this).data('cliente-id');
        var nombre = $(this).data('cliente-nombre');
        var apellidos = $(this).data('cliente-apellido');
        var correo = $(this).data('cliente-correo');
        var telefono = $(this).data('cliente-telefono');
        var direccion = $(this).data('cliente-direccion');
        var cedula = $(this).data('cliente-cedula');

        $('#id').val(clienteId);
        $('#nombre').val(nombre);
        $('#apellidos').val(apellidos);
        $('#correo').val(correo);
        $('#telefono').val(telefono);
        $('#direccion').val(direccion);
        $('#cedula').val(cedula);
    });

});

//Script que edita el cliente
$(document).ready(function () {
    $("#btnGuardarCliente").click(function () {
        var cliente_id = $("#id").val();
        var cliente_nombre = $("#nombre").val();
        var cliente_apellido = $("#apellidos").val();
        var cliente_correo = $("#correo").val();
        var cliente_telefono = $("#telefono").val();
        var cliente_direccion = $("#direccion").val();
        var cliente_cedula = $("#cedula").val();
        var urlClientes = document.getElementById('urlClientes').value;

        var entidad = {
            cliente_id: cliente_id,
            cliente_nombre: cliente_nombre,
            cliente_apellido: cliente_apellido,
            cliente_correo: cliente_correo,
            cliente_telefono: cliente_telefono,
            cliente_direccion: cliente_direccion,
            cliente_cedula: cliente_cedula
        };

        $.ajax({
            type: "POST",
            url: "/Cliente/EditarClienteAPI",
            data: entidad,
            success: function (result) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'CLIENTE ACTUALIZADO EXITOSAMENTE',
                    showConfirmButton: false,
                    timer: 1500,
                    width: 400
                });
                setTimeout(function () {
                    window.location.href = urlClientes;
                }, 1500);
            },
            error: function (error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Error al actualizar el cliente!'
                });
            }
        });

    });
});

//Script que crea el producto
$("#btncreacliente").click(function () {
    var cliente_cedula = $("#cedulanuevo").val().trim();;
    var cliente_nombre = $("#nombrenuevo").val().trim();;
    var cliente_apellidos = $("#apellidonuevo").val().trim();;
    var cliente_correo = $("#correonuevo").val().trim();;
    var cliente_telefono = $("#telefononuevo").val().trim();;
    var cliente_direccion = $("#direccionnuevo").val().trim();;
    var urlClientes = document.getElementById('urlClientes').value;

    if (!cliente_cedula || !cliente_nombre || !cliente_apellidos || !cliente_correo || !cliente_telefono || !cliente_direccion ) {
        Swal.fire({
            icon: 'error',
            title: 'Campos incompletos',
            text: 'Por favor completa todos los campos.',
        });
        return;
    }


    var entidadnueva = {
        cliente_cedula: cliente_cedula,
        cliente_nombre: cliente_nombre,
        cliente_apellidos: cliente_apellidos,
        cliente_correo: cliente_correo,
        cliente_telefono: cliente_telefono,
        cliente_direccion: cliente_direccion
    };


    $.ajax({
        type: "POST",
        url: "/Cliente/CreaCliente",
        data: entidadnueva,
        success: function (result) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'CLIENTE CREADO EXITOSAMENTE',
                showConfirmButton: false,
                timer: 1500,
                width: 400
            });
            setTimeout(function () {
                window.location.href = urlClientes;
            }, 1500);
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Error al crear el cliente!'
            });
        },
    });

});

//Script que borra el producto
$(document).ready(function () {
    $(".btnborracliente").click(function () {
        var cliente_id = $(this).data("cliente-id");
        var urlClientes = document.getElementById('urlClientes').value;
        Swal.fire({
            title: 'Esta seguro que desea borrar el cliente?',
            text: "No podras revertir los cambios!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, eliminar!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/Cliente/EliminaCliente',
                    data: { cliente_id: cliente_id },
                    success: function (response) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'CLIENTE ELIMINADO EXITOSAMENTE',
                            showConfirmButton: false,
                            timer: 1500,
                            width: 400
                        });
                        setTimeout(function () {
                            window.location.href = urlClientes;
                        }, 1500);
                    },
                    error: function (error) {
                        Swal.fire(
                            'Error',
                            'Ocurrió un error al eliminar el cliente.',
                            'error'
                        );
                    }
                });
            }
        });
    });
});
//
///////////////////////////////// SCRIPT DE CLIENTES FINAL /////////////////////////////////