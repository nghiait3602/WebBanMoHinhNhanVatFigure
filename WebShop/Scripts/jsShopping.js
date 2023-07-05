$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCard', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quatity = 1;
        var tquatity = $('#quantity_value').text();
        if (tquatity != "") {
            quatity = parseInt(tquatity);
        }
        $.ajax({
            url: '/ShoppingCart/AddToCart',
            type: 'POST',
            data: { id: id, quatity: quatity },
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout_items').html(rs.Count);
                    alert(rs.msg);
                }
            }
        });

    });

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có muốn xóa sản phẩn phẩm khỏi giỏ hàng?');
        if (conf) {
            $.ajax({
                url: '/ShoppingCart/Delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $('#checkout_items').html(rs.Count);
                        $('#trow_' + id).remove();
                        LoadCart();
                    }
                }
            });
        }
    });
    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var conf = confirm('Bạn có muốn xóa hết sản phẩm trong giỏ hàng?');
        if (conf) {
            DeleteAll();
        }
    });
    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = $('#Quantity_' + id).val();
        Update(id, quantity);
    });
});
function ShowCount() {
     $.ajax({
            url: '/ShoppingCart/ShowCount',
            type: 'GET',
         success: function (rs) {
              $('#checkout_items').html(rs.Count);
            }
        });

}
function DeleteAll() {
    $.ajax({
        url: '/ShoppingCart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });

}
function Update(id,quatity) {
    $.ajax({
        url: '/ShoppingCart/Update',
        type: 'POST',
        data: { id: id, quatity: quatity },
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            } else {
                alert("Sản phẩm không đủ số lượng!");
            }
        }
    });

}
function LoadCart() {
    $.ajax({
        url: '/ShoppingCart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
            ShowCount();

        }
    });

}