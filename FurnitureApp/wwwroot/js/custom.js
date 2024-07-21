(function () {
    'use strict';

    var tinyslider = function () {
        var el = document.querySelectorAll('.testimonial-slider');

        if (el.length > 0) {
            var slider = tns({
                container: '.testimonial-slider',
                items: 1,
                axis: "horizontal",
                controlsContainer: "#testimonial-nav",
                swipeAngle: false,
                speed: 700,
                nav: true,
                controls: true,
                autoplay: true,
                autoplayHoverPause: true,
                autoplayTimeout: 3500,
                autoplayButtonOutput: false
            });
        }
    };
    tinyslider();
});
// function to add increase and descrease btn click event
var sitePlusMinus = function () {

    var value,
        quantity = document.getElementsByClassName('quantity-container');

    function createBindings(quantityContainer) {
        var quantityAmount = quantityContainer.getElementsByClassName('quantity-amount')[0];
        var increase = quantityContainer.getElementsByClassName('increase')[0];
        var decrease = quantityContainer.getElementsByClassName('decrease')[0];
        increase.addEventListener('click', function (e) { increaseValue(e, quantityAmount); });
        decrease.addEventListener('click', function (e) { decreaseValue(e, quantityAmount); });
    }

    function init() {
        for (var i = 0; i < quantity.length; i++) {
            createBindings(quantity[i]);
        }
    };

    function increaseValue(event, quantityAmount) {
        value = parseInt(quantityAmount.value, 10);

        console.log(quantityAmount, quantityAmount.value);

        value = isNaN(value) ? 0 : value;
        value++;
        quantityAmount.value = value;
    }

    function decreaseValue(event, quantityAmount) {
        value = parseInt(quantityAmount.value, 10);

        value = isNaN(value) ? 0 : value;
        if (value > 0) value--;

        quantityAmount.value = value;
    }

    init();

};
sitePlusMinus();

// SignalR connection
$(() => {
    LoadPoductData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();
    connection.start();
    connection.on("LoadProducts", function () {
        LoadPoductData();
    });
    LoadPoductData();

    function LoadPoductData() {
        var productsAdmin = '';
        $.ajax({
            url: '/Admin/Products/Index?handler=GetProducts',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    productsAdmin += `
                        <tr>
							<td>
								${v.productName}
							</td>
							<td>
								${v.productDescription}
							</td>
							<td>
								${v.productPrice}
							</td>
							<td>
								${v.quantity}
							</td>
							<td>
								${v.productImage}
							</td>
							<td>
								<a href="/Admin/Products/Edit?id=${v.id}">Edit</a> |
								<a href="/Admin/Products/Details?id=${v.id}">Details</a> |
								<a href="/Admin/Products/Delete?id=${v.id}">Delete</a>
							</td>
						</tr>
                    `
                })
                $('#real-time-products').html(productsAdmin);
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});
