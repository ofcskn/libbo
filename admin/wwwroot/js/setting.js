$(document).on('click', '.languageButton', function () {
    var lang = $(this).data("value");
  
    //populateForm(
    //    settings.find((query) => query['lang'] === lang)['jsonData']
    //);

    var formData = new FormData();
    formData.append("lang", lang);


    $.ajax({
        url: "/admin/setting/get",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {
        console.log(data)
        populateForm(jQuery.parseJSON(data));
    });
});


var populateForm = function (data) {
    console.log(data)
    $("textarea[name='aboutUs']").summernote("code", data.aboutUs);
    $("input[name='phone']").val(data.phone);
    $("input[name='secondPhone']").val(data.secondPhone);
    $("input[name='email']").val(data.email);
    $("input[name='address']").val(data.address);
    $("input[name='facebook']").val(data.facebook);
    $("input[name='twitter']").val(data.twitter);
    $("input[name='instagram']").val(data.instagram);
    $("input[name='youtube']").val(data.youtube);
    $("input[name='pinterest']").val(data.pinterest);
    $("input[name='googlePlus']").val(data.googlePlus);
    $("textarea[name='headerText']").summernote("code", data.headerText);
    $("select[name='popupStatus']").val(data.popupStatus);
    $("textarea[name='popupText']").summernote("code", data.popupText);
    $("input[name='popupTitle']").val(data.popupTitle);

    $("input[name='mailContactTo']").val(data.mailContactTo);
    $("input[name='mailCMSUpdate']").val(data.mailCMSUpdate);
    $("input[name='mailHRTo']").val(data.mailHRTo);


    if (!isNull(data.logoWhite)) {
        $('#logoWhite').val(data.logoWhite);

        var imgHtml = "<a href='javascript:;'  data-filename='" + data.logoWhite + "' class='btn btn-icon-only btn-remove-image red'><i class='fa fa-times'></i></a>"
            + "<img src='" + mediaPath + data.logoWhite + "' />"
            + data.logoWhite;

        $('#logoWhiteFile').html(imgHtml);
    }
    else {
        $('#logoWhite').val("");
        $('#logoWhiteFile').html("");
    }

    if (!isNull(data.logoBlack)) {
        $('#logoBlack').val(data.logoBlack);

        var imgHtml = "<a href='javascript:;'  data-filename='" + data.logoBlack + "' class='btn btn-icon-only btn-remove-image-black red'><i class='fa fa-times'></i></a>"
            + "<img src='" + mediaPath + data.logoBlack + "' />"
            + data.logoBlack;

        $('#logoBlackFile').html(imgHtml);
    }
    else {
        $('#logoBlack').val("");
        $('#logoBlackFile').html("");
    }



    if (!isNull(data.header)) {
        $('#header').val(data.header);

        var imgHtml = "<a href='javascript:;'  data-filename='" + data.header + "' class='btn btn-icon-only btn-remove-header red'><i class='fa fa-times'></i></a>"
            + "<img src='" + mediaPath + data.header + "' />"
            + data.header;

        $('#headerFile').html(imgHtml);
    }
    else {
        $('#header').val("");
        $('#headerFile').html("");
    }

}

$(document).on("click", ".save", function () {



    var formData = new FormData();

    var lang = $(".btn-group .active input[type='radio']").val();

    formData.append("lang", lang);

    formData.append("jsonData", $("#formSetting").find("input,textarea,select").serializeJSON());



    $.ajax({
        url: "/admin/settings/insertUpdate",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {

    });

    modalNotification("Ayarlar başarıyla kaydedilmiştir.");
})

$(document).on('click', '.logoWhiteFileUpload', function () {
    $(this).fileupload({
        url: "/admin/media/insert",
        dataType: 'json',
        done: function (e, data) {

            $('#logoWhite').val(data.result.newFileName);

            var imgHtml = "<a href='javascript:;'  data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove-image red'><i class='fa fa-times'></i></a>"
                + "<img src='" + mediaPath + data.result.newFileName + "' />"
                + data.result.oldFileName;

            $('#logoWhiteFile').html("");
            $('#logoWhiteFile').html(imgHtml);


        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});


$(document).on('click', '.btn-remove-image', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.data('filename');

    var formData = new FormData();
    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {
        $('#logoWhite').val("");
        $('#logoWhiteFile').html("");
    });
});

$(document).on('click', '.logoBlackFileUpload', function () {
    $(this).fileupload({
        url: "/admin/media/insert",
        dataType: 'json',
        done: function (e, data) {

            $('#logoBlack').val(data.result.newFileName);

            var imgHtml = "<a href='javascript:;'  data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove-image-black red'><i class='fa fa-times'></i></a>"
                + "<img src='" + mediaPath + data.result.newFileName + "' />"
                + data.result.oldFileName;

            $('#logoBlackFile').html("");
            $('#logoBlackFile').html(imgHtml);


        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});

$(document).on('click', '.btn-remove-image-black', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.data('filename');

    var formData = new FormData();
    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {
        $('#logoBlack').val("");
        $('#logoBlackFile').html("");
    });
});

$(document).on('click', '.headerFileUpload', function () {
    $(this).fileupload({
        url: "/admin/media/insert",
        dataType: 'json',
        done: function (e, data) {

            $('#header').val(data.result.newFileName);

            var imgHtml = "<a href='javascript:;'  data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove-image red'><i class='fa fa-times'></i></a>"
                + "<img src='" + mediaPath + data.result.newFileName + "' />"
                + data.result.oldFileName;

            $('#headerFile').html("");
            $('#headerFile').html(imgHtml);


        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});

$(document).on('click', '.btn-remove-image', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.data('filename');

    var formData = new FormData();
    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {
        $('#header').val("");
        $('#headerFile').html("");
    });
});
