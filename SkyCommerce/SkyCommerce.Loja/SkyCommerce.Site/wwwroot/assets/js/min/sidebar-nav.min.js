$(window).scroll(function () {
    $(this).scrollTop() > 1 ? $(".navbar-hero").addClass("sticky") : $(".navbar-hero").removeClass("sticky")
}), $(function () {
    $(".cart-sidebar-toggle").click(function () {
        $(this).toggleClass("active"), $(".overly-mask").toggleClass("is-visible"), $(".cart-sidebar").toggleClass("is-visible"), $(".cartMenu ").toggleClass("open"), $("body").toggleClass("modal-open"), $(".megamenu").hasClass("stuck") ? $(".cart-sidebar").addClass("hasTopBar") : $(".cart-sidebar").removeClass("hasTopBar")
    }), $(".menu-sidebar-toggle").click(function () {
        $(this).toggleClass("active"), $(".menu-overly-mask").toggleClass("is-visible"), $(".menu-sidebar").toggleClass("is-visible")
    }), $(".menu-close-trigger, .menu-overly-mask").click(function () {
        $(".menu-overly-mask").toggleClass("is-visible"), $(".menu-sidebar-toggle").toggleClass("active"), $(".menu-sidebar").toggleClass("is-visible")
    }), $(".overly-mask, .cart-close-trigger").click(function () {
        $(".overly-mask").toggleClass("is-visible"), $(".cart-sidebar").toggleClass("is-visible"), $(".cart-sidebar-toggle").toggleClass("active"), $(".cartMenu ").toggleClass("open"), $("body").toggleClass("modal-open")
    }), $(".search-trigger").on("click", function (s) {
        $(".search-overly-mask").toggleClass("open"), s.preventDefault()
    }), $(".search-overly-close-trigger").on("click", function (s) {
        $(".search-overly-mask").removeClass("open"), s.preventDefault()
    })
});