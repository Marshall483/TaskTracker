
function coloriseSignInIcon(iconClass, textClass) {
    $('#sign-in-icon').removeClass(textClass);
    $('#sign-in-text').removeClass(iconClass);
    $('#sign-in-icon').addClass(iconClass);
    $('#sign-in-text').addClass(textClass);
}

window.addEventListener('scroll', function () {
    elem = this.document.getElementById('fixibleHeader');
    y = elem.getBoundingClientRect().top;
    if (y == 0 && pageYOffset < 130) {
        $('#fixibleHeader').removeClass('header-fixed')
    }
    if (y <= 0 && this.pageYOffset > 130) {
        $('#fixibleHeader').addClass('header-fixed')
    }
});
document.getElementById('navbarToggler').addEventListener('click', function () {
    if ($('#navbarToggler').hasClass('active')) {
        $('#navbarToggler').removeClass('active');
        $('#headerMain').removeClass('active');
        $('#navbar-main-container').removeClass('active');
        $('#navbar-main-top').removeClass('active');
        $('#fixibleHeader').removeClass('active');
    } else {
        $('#navbarCollapseToggler').removeClass('active');
        $('#headerRight').removeClass('active');
        $('#navbarToggler').addClass('active');
        $('#headerMain').addClass('active');
        $('#navbar-main-container').addClass('active');
        $('#navbar-main-top').addClass('active');
        $('#fixibleHeader').addClass('active');
    }
});
document.getElementById('navbarCollapseToggler').addEventListener('click', function () {
    if ($('#navbarCollapseToggler').hasClass('active')) {
        $('#navbarCollapseToggler').removeClass('active');
        $('#headerRight').removeClass('active');

    } else {
        $('#navbarToggler').removeClass('active');
        $('#headerMain').removeClass('active');
        $('#navbar-main-container').removeClass('active');
        $('#navbar-main-top').removeClass('active');
        $('#fixibleHeader').removeClass('active');
        $('#navbarCollapseToggler').addClass('active');
        $('#headerRight').addClass('active');

    }
});

function switchLogReg() {
    let hostUrl = window.location.protocol + '//' + window.location.host + '/';
    if ($('#logRegSwitch').data("currentpage") == "reg") {
        $.ajax({
            url: hostUrl + "RegLog/GetLogPartial",
            method: "GET",
            success: function (data) {
                console.log(hostUrl + "RegLog/GetLogPartial")
                $('#regLogWraper').empty().append(data);
            }
        })
        return

    }
    if ($('#logRegSwitch').data("currentpage") == "log") {
        $.ajax({
            url: hostUrl + "RegLog/GetRegPartial",
            method: "GET",
            success: function (data) {
                console.log(hostUrl + "RegLog/GetRegPartial")
                $('#regLogWraper').empty().append(data);
            }
        })
        return
    }
}

function checkCustomCheckBox() {
    if ($('#rememberMeCheckBox').hasClass('checked')) {
        $('#hiddenCheckbox').prop('checked', true);
        $('#rememberMeCheckBox').removeClass('checked')
        console.log($('#hiddenCheckbox').is(':checked'))
    } else {
        $('#hiddenCheckbox').prop('checked', false);
        $('#rememberMeCheckBox').addClass('checked');
        console.log($('#hiddenCheckbox').is(':checked'))
    }
}