(function ($) {
    'use strict';

     //Header Search
    if($('.search-box-outer').length) {
        $('.search-box-outer').on('click', function() {
            $('body').addClass('search-active');
        });
        $('.close-search').on('click', function() {
            $('body').removeClass('search-active');
        });
    }

   

    // Mobile Menu
    $('.mobile-menu nav').meanmenu({
        meanScreenWidth: "991",
        meanMenuContainer: ".mobile-menu",
        meanMenuOpen: "<span></span> <span></span> <span></span>",
        onePage: false,
    });

    // sticky
    var wind = $(window);
    var sticky = $('#sticky-header');
    wind.on('scroll', function () {
        var scroll = wind.scrollTop();
        if (scroll < 100) {
            sticky.removeClass('sticky');
        } else {
            sticky.addClass('sticky');
        }
    });


   

    // Case Study Active
    $('.slider').owlCarousel({
        loop: true,
        autoplay: true,
        smartSpeed: 1500,
        autoplayTimeout: 1500,
        dots: false,
        nav: true,
        center: true,
        navText: ["<i class='bi bi-chevron-left'></i>", "<i class='bi bi-chevron-right'></i>"],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            768: {
                items: 1
            },
            992: {
                items: 1
            },
            1200: {
                items: 1
            },
            1920: {
                items: 1
            }
        }
    })


    // brand Active
    $('.news-food-slider').owlCarousel({
        loop: true,
        autoplay: true,
        smartSpeed: 1500,
        autoplayTimeout: 10000,
        dots: false,
        nav: false,
        navText: ["<i class='bi bi-arrow-left''></i>", "<i class='bi bi-arrow-right''></i>"],
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            },
            1200: {
                items: 3
            },
            1920: {
                items: 3
            }
        }
    })


    // Barnd Active

    $('.brand-slider').owlCarousel({
        loop: true,
        autoplay: true,
        smartSpeed: 1500,
        autoplayTimeout: 1500,
        dots: false,
        nav: false,
        center: false,
        margin: 15,
        navText: ["<i class='bi bi-arrow-left''></i>", "<i class='bi bi-arrow-right''></i>"],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 5
            },
            1200: {
                items: 6
            },
            1920: {
                items: 6
            }
        }
    }) 
    
    
    
    // Barnd Active

    $('.brand-slider2').owlCarousel({
        loop: true,
        autoplay: true,
        smartSpeed: 1500,
        autoplayTimeout: 1500,
        dots: false,
        nav: false,
        center: false,
        margin: 30,
        navText: ["<i class='bi bi-arrow-left''></i>", "<i class='bi bi-arrow-right''></i>"],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            },
            1200: {
                items: 4
            },
            1920: {
                items: 4
            }
        }
    });

    
    

    /*---------------------
    WOW active js 
    --------------------- */
    new WOW().init();

    // counterUp
    $('.counter').counterUp({
        delay: 5,
        time: 1500
    });

    

    // Venubox

    $('.venobox').venobox({

        numeratio: true,

        infinigall: true

    });


    /*--------------------------
     scrollUp
    ---------------------------- */
    $.scrollUp({
        scrollText: '<i class="fa fa-angle-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    })


    jQuery(document).ready(function ($) {
        "use strict";

        // =======< accordion js >========
        $(".accordion > li:eq(0) a").addClass("active").next().slideDown();
        $('.accordion a').on('click', function (j) {
            var dropDown = $(this).closest("li").find("p");

            $(this).closest(".accordion").find("p").not(dropDown).slideUp();

            if ($(this).hasClass("active")) {
                $(this).removeClass("active");
            } else {
                $(this).closest(".accordion").find("a.active").removeClass("active");
                $(this).addClass("active");
            }

            dropDown.stop(false, true).slideToggle();

            j.preventDefault();
        });

    });





        // Testimonial Tab
        $(document).ready(function() {

    
            const tabs= document.querySelectorAll('.testimonial-btn');
            const all_content= document.querySelectorAll('.testimonial-tab-content')
    
            tabs.forEach((tab, index)=>{
                tab.addEventListener('click', ()=>{
                    tabs.forEach(tab=>{tab.classList.remove('active')});
                    tab.classList.add('active');
    
                    all_content.forEach(content=>{content.classList.remove('active')});
                    all_content[index].classList.add('active');
                });
            });
        });
        
        
        //Testimonial tab
    
        $(document).ready(function() {
    
        
            const tabs= document.querySelectorAll('.testimonial-btn');
            const all_content= document.querySelectorAll('.testimonial-content-2')
    
            tabs.forEach((tab, index)=>{
                tab.addEventListener('click', ()=>{
                    tabs.forEach(tab=>{tab.classList.remove('active')});
                    tab.classList.add('active');
    
                    all_content.forEach(content=>{content.classList.remove('active')});
                    all_content[index].classList.add('active');
                });
            });
        });
        
        //Shop Details Tab
    
        $(document).ready(function() {
    
        
            const tabs= document.querySelectorAll('.info-tab-btn button');
            const all_content= document.querySelectorAll('.tab-contents')
    
            tabs.forEach((tab, index)=>{
                tab.addEventListener('click', ()=>{
                    tabs.forEach(tab=>{tab.classList.remove('active')});
                    tab.classList.add('active');
    
                    all_content.forEach(content=>{content.classList.remove('active')});
                    all_content[index].classList.add('active');
                });
            });
        });



        //Curser animation

    $(document).ready(function() {

        var curser = document.querySelector(".curser");
        var curser2 = document.querySelector(".curser2");

        document.addEventListener("mousemove", function(e){
            curser.style.cssText = curser2.style.cssText = "left: " + e.clientX + "px; top: " + e.clientY + "px;";
        });
    });

        

                
            // active class & remove class

            $(document).ready(function() {
                let buttons = document.querySelectorAll('.shop-list-left i');

                buttons.forEach(button => {
                    button.addEventListener('click', function () {
                        buttons.forEach(btn => btn.classList.remove('active'));
                        this.classList.add('active');        
                    });
                });
            });




            $(document).ready(function() {

                function getTimeRemaining(endtime) {
                    var t = Date.parse(endtime) - Date.now();
                    var seconds = Math.floor((t / 1000) % 60);
                    var minutes = Math.floor((t / 1000 / 60) % 60);
                    var hours = Math.floor((t / (1000 * 60 * 60)) % 24);
                    var days = Math.floor(t / (1000 * 60 * 60 * 24));
                    return {
                    'total': t,
                    'days': days,
                    'hours': hours,
                    'minutes': minutes,
                    'seconds': seconds
                    };
                }
    
                function initializeClock(id, endtime) {
                    var clock = document.getElementById(id);
                    var daysSpan = clock.querySelector('.days');
                    var hoursSpan = clock.querySelector('.hours');
                    var minutesSpan = clock.querySelector('.minutes');
                    var secondsSpan = clock.querySelector('.seconds');
    
                    function updateClock() {
                    var t = getTimeRemaining(endtime);
    
                    daysSpan.innerHTML = t.days;
                    hoursSpan.innerHTML = ('0' + t.hours).slice(-2);
                    minutesSpan.innerHTML = ('0' + t.minutes).slice(-2);
                    secondsSpan.innerHTML = ('0' + t.seconds).slice(-2);
    
                    if (t.total <= 0) {
                        clearInterval(timeinterval);
                    }
                    }
    
                    updateClock();
                    var timeinterval = setInterval(updateClock, 1000);
                }
                // count down timer:
                var deadline = new Date(Date.now() + 385 * 23 * 59 * 59 * 1000);
                initializeClock('clockdiv', deadline);
            });


            


    // scroll up

    if($('.prgoress_indicator path').length){
        var progressPath = document.querySelector('.prgoress_indicator path');
        var pathLength = progressPath.getTotalLength();
        progressPath.style.transition = progressPath.style.WebkitTransition = 'none';
        progressPath.style.strokeDasharray = pathLength + ' ' + pathLength;
        progressPath.style.strokeDashoffset = pathLength;
        progressPath.getBoundingClientRect();
        progressPath.style.transition = progressPath.style.WebkitTransition = 'stroke-dashoffset 10ms linear';
        var updateProgress = function () {
          var scroll = $(window).scrollTop();
          var height = $(document).height() - $(window).height();
          var progress = pathLength - (scroll * pathLength / height);
          progressPath.style.strokeDashoffset = progress;
        }
        updateProgress();
        $(window).on('scroll', updateProgress);
        var offset = 250;
        var duration = 550;
        jQuery(window).on('scroll', function () {
          if (jQuery(this).scrollTop() > offset) {
            jQuery('.prgoress_indicator').addClass('active-progress');
          } else {
            jQuery('.prgoress_indicator').removeClass('active-progress');
          }
        });
        jQuery('.prgoress_indicator').on('click', function (event) {
          event.preventDefault();
          jQuery('html, body').animate({ scrollTop: 0 }, duration);
          return false;
        });
    }





            // Preloader
        $(document).ready(function() {
            setTimeout(function(){
                $('.loader_bg').fadeToggle();
            }, 1000);
        });	


})(jQuery);