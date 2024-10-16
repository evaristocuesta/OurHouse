import PhotoSwipeLightbox from 'https://cdn.jsdelivr.net/npm/photoswipe@5.4.4/dist/photoswipe-lightbox.esm.min.js';

window.onload = (event) => {

    var swiper = new Swiper(".mySwiper", {
        slidesPerView: "auto",
        spaceBetween: 10,
        grabCursor: true,
        pagination: {
            el: ".swiper-pagination",
            clickable: true,
        },
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        keyboard: {
            enabled: true,
        }
    });

    const lightbox = new PhotoSwipeLightbox({
        gallery: ".mySwiper",
        children: "a",
        pswpModule: () => import('https://cdn.jsdelivr.net/npm/photoswipe@5.4.4/dist/photoswipe.esm.js')
    });

    lightbox.init();
};