
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

//Script que carga los datos del cliente en el modal
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

    if (!cliente_cedula || !cliente_nombre || !cliente_apellidos || !cliente_correo || !cliente_telefono || !cliente_direccion) {
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
//
//
///////////////////////////////// SCRIPT DE USUARIOS /////////////////////////////////

//Script que carga los datos del usuario en el modal  y llena el select de roles
$(document).ready(function () {
    $('.btn-modificar-usuario').on('click', function () {
        var usuarioId = $(this).data('usuario-id');
        var nombre = $(this).data('usuario-nombre');
        var cedula = $(this).data('usuario-cedula');
        var correo = $(this).data('usuario-correo');
        var rolid = $(this).data('usuario-rolid');
        var roldescripcion = $(this).data('usuario-roldescripcion');


        $('#id').val(usuarioId);
        $('#nombre').val(nombre);
        $('#correo').val(correo);
        $('#rolid').val(rolid);
        $('#roldescripcion').val(roldescripcion);
        $('#cedula').val(cedula);


        $.ajax({
            url: '/Usuario/CargarRoles',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var select = $('#rol');
                select.empty();

                $.each(data, function (index, rol) {
                    var option = $('<option>').text(rol.rol_descripcion).val(rol.rol_descripcion);

                    option.attr('data-rol-id', rol.rol_id);

                    select.append(option);

                    if (rol.rol_descripcion === roldescripcion) {
                        option.prop('selected', true);
                    }
                });
            },
        });

    });




    //Script que edita el usuario
    $(document).ready(function () {
        $("#btnGuardarUsuario").click(function () {
            var usuario_id = $("#id").val();
            var usu_nombre = $("#nombre").val();
            var usu_identificacion = $("#cedula").val();
            var usu_correo = $("#correo").val();

            var selectedRolId = $('#rol option:selected').data('rol-id');
            var urlUsuarios = document.getElementById('urlUsuarios').value;

            var entidad = {
                usuario_id: usuario_id,
                usu_nombre: usu_nombre,
                usu_identificacion: usu_identificacion,
                usu_correo: usu_correo,
                rol_id: selectedRolId,
            };



            $.ajax({
                type: "POST",
                url: "/Usuario/EditarUsuarioAPI",
                data: entidad,
                success: function (result) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'USUARIO ACTUALIZADO EXITOSAMENTE',
                        showConfirmButton: false,
                        timer: 1500,
                        width: 400
                    });
                    setTimeout(function () {
                        window.location.href = urlUsuarios;
                    }, 1500);
                },
                error: function (error) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Error al actualizar el usuario!'
                    });
                }
            });


        });



        //Script que crea el usuario
        $("#btncreausuario").click(function () {
            var usu_nombre = $("#nombrenuevo").val().trim();;
            var usu_identificacion = $("#identificacionnuevo").val().trim();;
            var usu_correo = $("#correonuevo").val().trim();;
            var rol_id = $('#rol2 option:selected').val().trim();;
            var urlUsuarios = document.getElementById('urlUsuarios').value;


            if (!usu_nombre || !usu_identificacion || !usu_correo || !rol_id) {
                Swal.fire({
                    icon: 'error',
                    title: 'Campos incompletos',
                    text: 'Por favor completa todos los campos.',
                });
                return;
            }


            var entidadnueva = {
                usu_nombre: usu_nombre,
                usu_identificacion: usu_identificacion,
                usu_correo: usu_correo,
                rol_id: rol_id
            };
            console.log(entidadnueva)


            $.ajax({
                type: "POST",
                url: "/Usuario/CrearUsuario",
                data: entidadnueva,
                success: function (result) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'USUARIO CREADO EXITOSAMENTE',
                        showConfirmButton: false,
                        timer: 1500,
                        width: 400
                    });
                    setTimeout(function () {
                        window.location.href = urlUsuarios;
                    }, 1500);
                },
                error: function (error) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Error al crear el usuario!'
                    });
                },
            });

        });
       
    });
    //Carga roles en modal nuevo usuario
    $.ajax({
        url: '/Usuario/CargarRoles',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var select = $('#rol2');
            select.empty();

            $.each(data, function (index, rol) {
                var option = $('<option>')
                    .val(rol.rol_id)
                    .text(rol.rol_descripcion);

                select.append(option);
            });
        },
    });

});
//Script que borra el usuario
$(document).ready(function () {
    $(".btnborrausuario").click(function () {
        var usuario_id = $(this).data("usuario-id");
        var urlUsuarios = document.getElementById('urlUsuarios').value;
        Swal.fire({
            title: 'Esta seguro que desea borrar el usuario?',
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
                    url: '/Usuario/EliminaUsuario',
                    data: { usuario_id: usuario_id },
                    success: function (response) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'USUARIO ELIMINADO EXITOSAMENTE',
                            showConfirmButton: false,
                            timer: 1500,
                            width: 400
                        });
                        setTimeout(function () {
                            window.location.href = urlUsuarios;
                        }, 1500);
                    },
                    error: function (error) {
                        Swal.fire(
                            'Error',
                            'Ocurrió un error al eliminar el usuario.',
                            'error'
                        );
                    }
                });
            }
        });
    });
});

///////////////////////////////// SCRIPT DE USUARIOS FINAL /////////////////////////////////

///////////////////////////////// SCRIPT DE PROVEEDORES /////////////////////////////////

$(document).ready(function () {
    $('.btn-modificar-proveedor').on('click', function () {
        var proveedor_id = $(this).data('proveedor-id');
        var cedula = $(this).data('proveedor-cedula');
        var nombre = $(this).data('proveedor-nombre');
        var apellido = $(this).data('proveedor-apellido');
        var correo = $(this).data('proveedor-correo');
        var telefono = $(this).data('proveedor-telefono');
        var direccion = $(this).data('proveedor-direccion');

    
        var telefonoNumero = parseInt(telefono, 10); 
        $('#idproveedor').val(proveedor_id);
        $('#cedulaproveedor').val(cedula);
        $('#nombreproveedor').val(nombre);
        $('#apellidoproveedor').val(apellido);
        $('#correoproveedor').val(correo);
        $('#telefonoproveedor').val(telefonoNumero);
        $('#direccionproveedor').val(direccion);
    });

});

//Script que edita el proveedor
$(document).ready(function () {
    $("#btnGuardarProveedor").click(function () {
        var proveedor_id = $("#idproveedor").val();
        var proveedor_nombre = $("#nombreproveedor").val(); 
        var proveedor_cedula = $("#cedulaproveedor").val();
        var proveedor_apellido = $("#apellidoproveedor").val();
        var proveedor_correo = $("#correoproveedor").val();
        var proveedor_telefono = $("#telefonoproveedor").val();
        var proveedor_direccion = $("#direccionproveedor").val();
        var urlProveedores = document.getElementById('urlProveedores').value;

        var entidadproveedor = {
            proveedor_id: proveedor_id,
            proveedor_cedula: proveedor_cedula,
            proveedor_nombre: proveedor_nombre,
            proveedor_apellido: proveedor_apellido,
            proveedor_correo: proveedor_correo,
            proveedor_telefono: proveedor_telefono,
            proveedor_direccion: proveedor_direccion
        };

        
        

        $.ajax({
            type: "POST",
            url: "/Proveedor/EditarProductoAPI",
            data: entidadproveedor,
            success: function (result) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'PROVEEDOR ACTUALIZADO EXITOSAMENTE',
                    showConfirmButton: false,
                    timer: 1500,
                    width: 400
                });
                setTimeout(function () {
                    window.location.href = urlProveedores;
                }, 1500);
            },
            error: function (error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Error al actualizar el proveedor!'
                });
            }
        });
    });
});

//Script que crea el proveedor
$("#btncreaproveedor").click(function () {
    var proveedor_nombre = $("#nombreproveedornuevo").val().trim();;
    var proveedor_cedula = $("#cedulaproveedornuevo").val().trim();;
    var proveedor_telefono = $("#telefonoproveedornuevo").val().trim();;
    var proveedor_apellido = $("#apellidoproveedornuevo").val().trim();;
    var proveedor_correo = $("#correoproveedornuevo").val().trim();;
    var proveedor_direccion = $("#direccionproveedornuevo").val().trim();;
    var urlProveedores = document.getElementById('urlProveedores').value;


    if (!proveedor_nombre || !proveedor_cedula || !proveedor_telefono || !proveedor_apellido || !proveedor_correo || !proveedor_direccion) {
        Swal.fire({
            icon: 'error',
            title: 'Campos incompletos',
            text: 'Por favor completa todos los campos.',
        });
        return;
    }


    var entidadnueva = {
        proveedor_nombre: proveedor_nombre,
        proveedor_cedula: proveedor_cedula,
        proveedor_telefono: proveedor_telefono,
        proveedor_apellido: proveedor_apellido,
        proveedor_correo: proveedor_correo,
        proveedor_direccion: proveedor_direccion
    };
    


    $.ajax({
        type: "POST",
        url: "/Proveedor/CrearProveedor",
        data: entidadnueva,
        success: function (result) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'PROVEEDOR CREADO EXITOSAMENTE',
                showConfirmButton: false,
                timer: 1500,
                width: 400
            });
            setTimeout(function () {
                window.location.href = urlProveedores;
            }, 1500);
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Error al crear el proveedor!'
            });
        },
    });

});

//Script que borra el proveedor
$(document).ready(function () {
    $(".btnborraproveedor").click(function () {
        var proveedor_id = $(this).data("proveedor-id");
        var urlProveedores = document.getElementById('urlProveedores').value;
        Swal.fire({
            title: 'Esta seguro que desea borrar el proveedor?',
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
                    url: '/Proveedor/EliminaProveedor',
                    data: { proveedor_id: proveedor_id },
                    success: function (response) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'PROVEEDOR ELIMINADO EXITOSAMENTE',
                            showConfirmButton: false,
                            timer: 1500,
                            width: 400
                        });
                        setTimeout(function () {
                            window.location.href = urlProveedores;
                        }, 1500);
                    },
                    error: function (error) {
                        Swal.fire(
                            'Error',
                            'Ocurrió un error al eliminar el proveedor.',
                            'error'
                        );
                    }
                });
            }
        });
    });
});

///////////////////////////////// SCRIPT DE PROVEEDORES FINAL /////////////////////////////////

///////////////////////////////// SCRIPT DE ROLES  /////////////////////////////////
$(document).ready(function () {
    $('.btn-modificar-roles').on('click', function () {
        var rol_id = $(this).data('rol-id');
        var rol_descripcion = $(this).data('rol-descripcion');

        $('#rolid').val(rol_id);
        $('#roldescripcion').val(rol_descripcion);

    });

});

//Script que edita el rol
$(document).ready(function () {
    $("#btnGuardarRol").click(function () {
        var rol_id = $("#rolid").val();
        var rol_descripcion = $("#roldescripcion").val();
        var urlRoles= document.getElementById('urlRoles').value;

        var entidadrol = {
            rol_id: rol_id,
            rol_descripcion: rol_descripcion
        };




        $.ajax({
            type: "POST",
            url: "/Rol/EditarRolAPI",
            data: entidadrol,
            success: function (result) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'ROL ACTUALIZADO EXITOSAMENTE',
                    showConfirmButton: false,
                    timer: 1500,
                    width: 400
                });
                setTimeout(function () {
                    window.location.href = urlRoles;
                }, 1500);
            },
            error: function (error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Error al actualizar el rol!'
                });
            }
        });

    });
});

//Script que crea el rol
$("#btncrearol").click(function () {
    var rol_descripcion = $("#descripcionrolnuevo").val().trim();;
    var urlRoles = document.getElementById('urlRoles').value;


    if (!rol_descripcion) {
        Swal.fire({
            icon: 'error',
            title: 'Campos incompletos',
            text: 'Por favor completa todos los campos.',
        });
        return;
    }


    var entidadnueva = {
        rol_descripcion: rol_descripcion
    };



    $.ajax({
        type: "POST",
        url: "/Rol/CrearRol",
        data: entidadnueva,
        success: function (result) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'ROL CREADO EXITOSAMENTE',
                showConfirmButton: false,
                timer: 1500,
                width: 400
            });
            setTimeout(function () {
                window.location.href = urlRoles;
            }, 1500);
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Error al crear el rol!'
            });
        },
    });

});

//Script que borra el rol
$(document).ready(function () {
    $(".btnborrarol").click(function () {
        var rol_id = $(this).data("rol-id");
        var urlRoles = document.getElementById('urlRoles').value;
        Swal.fire({
            title: 'Esta seguro que desea borrar el rol?',
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
                    url: '/Rol/EliminarRolAPI',
                    data: { rol_id: rol_id },
                    success: function (response) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'ROL ELIMINADO EXITOSAMENTE',
                            showConfirmButton: false,
                            timer: 1500,
                            width: 400
                        });
                        setTimeout(function () {
                            window.location.href = urlRoles;
                        }, 1500);
                    },
                    error: function (error) {
                        Swal.fire(
                            'Error',
                            'Ocurrió un error al eliminar el rol.',
                            'error'
                        );
                    }
                });
            }
        });
    });
});
///////////////////////////////// SCRIPT DE ROLES FINAL /////////////////////////////////