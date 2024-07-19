(function() {
	'use strict';

	var tinyslider = function() {
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

	


	var sitePlusMinus = function() {

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
	        for (var i = 0; i < quantity.length; i++ ) {
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
		var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();
		connection.start();
		LoadPostData();
		connection.on("LoadPosts", function () {
			LoadPostData();
		});

		function LoadPostData() {
			var posts = '';
			$.ajax({
				url: '/Post/GetPosts',
				method: 'GET',
				success: (result) => {
					$.each(result.$values, (k, v) => {
						posts += `
                        <div class="col-12 col-md-4 mb-3">
                        <div class="card h-100">
                            <img src="https://media.istockphoto.com/id/1369150014/vi/vec-to/tin-n%C3%B3ng-v%E1%BB%9Bi-n%E1%BB%81n-b%E1%BA%A3n-%C4%91%E1%BB%93-th%E1%BA%BF-gi%E1%BB%9Bi-vect%C6%A1.jpg?s=2048x2048&w=is&k=20&c=3zzs18kgKD8GqBKJ4lJkg5xejtunDasZ982Sjm0fsnM=" class="card-img-top" style="max-height: 200px;">
                            <div class="d-flex flex-column card-body">
                                <h5 class="card-title">${v.title}</h5>
                                <p class="card-text"> ${v.content.substring(0, v.content.length > 200 ? 200 : v.content.length)}</p>
                                <span class="d-flex justify-content-end align-items-end" style="flex: 1">
                                    <a asp-controller="Post" asp-action="Detail" asp-route-id="${v.postId}" class="btn btn-primary">
                                        View Details
                                    </a>
                                </span>
                            </div>
                        </div>
                    </div>
                    `
					})
					$('#real-time-posts').html(posts);
				},
				error: (error) => {
					console.log(error);
				}
			});
		}
})()
