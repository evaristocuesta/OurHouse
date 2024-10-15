window.onload = (event) => {

    var mainSwiper = new Swiper(".mainSwiper", {
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

    var thumbsSwiper = new Swiper(".thumbsSwiper", {
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

    var slideSwiper = new Swiper(".slideSwiper", {
        spaceBetween: 10,
        grabCursor: true,
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        thumbs: {
            swiper: thumbsSwiper
        }
    });

    const photos = document.querySelectorAll(".mainSwiper .swiper-slide");

    if (photos) {
        console.log(photos);

        photos.forEach((photo) => {
            photo.onclick = (event) => {
                console.log("se ejecuta el evento");

                const myModal = new bootstrap.Modal('#modalGallery', {
                    keyboard: false
                });

                myModal.show();
            };
        });
    }
};
