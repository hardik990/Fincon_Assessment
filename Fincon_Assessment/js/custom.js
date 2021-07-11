$(document).ready(function() {
    
    
    if ($(window).width() < 768) {
      $this = $(this);
      $nav = $('body');
      $nav.addClass('toggle');
      $('.toggle-nav').click(function() {
        $this = $(this);
        $nav = $('body');
        $nav.toggleClass('toggle');
      });
    }
    else{
      $('.toggle-nav').click(function() {
        $this = $(this);
        $nav = $('body');
        $nav.toggleClass('toggle');
      });
    }

     $('.body-part').click(function() {
         $nav.addClass('toggle');
     });
     $submenu = $('.child-menu-ul');
     $('.child-menu a').on('click', function(e) {
         e.preventDefault();
         $this = $(this);
         $parent = $this.next();
         $tar = $('.child-menu-ul');
         if (!$parent.hasClass('active')) {
             $tar.removeClass('active').slideUp('fast');
             $parent.addClass('active').slideDown('fast');
         } else {
             $parent.removeClass('active').slideUp('fast');
         }
     });

    
});

$(document).ready(function() {
    $('#dropdownMenuButton').dropdown();

    /*$('.row-info-trigger').click(function() {
        alert('abc');
        $(this).parent('tr').addClass('abc');
    });*/
});

jQuery(document).ready(function (e) {
    e(".checked-box").hover(function () {
        e(this).hasClass("active") ? e(this).removeClass("active") : e(this).addClass("active")
    }), e(".checked-box input").change(function () {
        e(this).is(":cheked ") ? e(this).parent().parent().parent().parent().addClass("purple-border") : e(this).parent().parent().parent().parent().removeClass("purple-border")
    })
});