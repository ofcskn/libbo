
window.addEventListener('DOMContentLoaded', function () {
    var avatar = document.getElementById('avatar');
    var image = document.getElementById('image');
    var input = document.getElementById('input');
    var $progress = $('.progress');
    var $progressBar = $('.progress-bar');
    var $alert = $('.alert');
    var $modal = $('#modal');
    var $dropImage = $('.drop-text');
    var $cropForm = $('.cropForm');
    var cropper;

    //File Drop Script
    $('.cropForm').filedrop({
        drop: function (a) {
            a.stopPropagation();
            a.preventDefault();
            var files = a.dataTransfer.files;
            var done = function (url) {
                input.value = '';
                image.src = url;
                $alert.hide();
                $modal.modal('show');
            };
            var reader;
            var file;
            var url;

            if (files && files.length > 0) {
                file = files[0];

                if (URL) {
                    done(URL.createObjectURL(file));
                } else if (FileReader) {
                    reader = new FileReader();
                    reader.onload = function (e) {
                        done(reader.result);
                    };
                    reader.readAsDataURL(file);
                }
            }
        },
    });


    $('[data-toggle="tooltip"]').tooltip();

    input.addEventListener('change', function (e) {
        var files = e.target.files;
        var done = function (url) {
            input.value = '';
            image.src = url;
            $alert.hide();
            $modal.modal('show');
        };
        var reader;
        var file;
        var url;

        if (files && files.length > 0) {
            file = files[0];

            if (URL) {
                done(URL.createObjectURL(file));
            } else if (FileReader) {
                reader = new FileReader();
                reader.onload = function (e) {
                    done(reader.result);
                };
                reader.readAsDataURL(file);
            }
        }
    });

    $modal.on('shown.bs.modal', function () {
        cropper = new Cropper(image, {
            //aspectRatio: 16 / 9,
            viewMode: 3,
        });
    }).on('hidden.bs.modal', function () {
        cropper.destroy();
        cropper = null;
    });

    document.getElementById('crop').addEventListener('click', function () {
        var canvas;

        $modal.modal('hide');

        if (cropper) {
            canvas = cropper.getCroppedCanvas({
                width: 1920,
                height: 1080,
            });
            avatar.src = canvas.toDataURL();
            $cropForm.removeClass('text-center');
            $dropImage.hide();
            $progress.show();          
            $alert.removeClass('alert-success alert-warning');
            canvas.toBlob(function (blob) {
                var formData = new FormData();
                console.log(formData);
                formData.append('avatar', blob, 'avatar.jpg');               
                $.ajax({
                    url: "/admin/media/manage",
                    method: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,

                    xhr: function () {
                        var xhr = new XMLHttpRequest();

                        xhr.upload.onprogress = function (e) {
                            var percent = '0';
                            var percentage = '0%';

                            if (e.lengthComputable) {
                                percent = Math.round((e.loaded / e.total) * 100);
                                percentage = percent + '%';
                                $progressBar.width(percentage).attr('aria-valuenow', percent).text(percentage);
                            }
                        };

                        return xhr;
                    },

                    success: function (data) {
                        if (data == "refresh") {
                            setTimeout(function () { $alert.show().addClass('alert-success').text("Yükleme işlemi başarıyla gerçekleşmiştir.") }, 500);
                            setTimeout(function () { window.location.href = "/admin/media/list"; }, 2000);
                        }
                    },

                    error: function (data) {
                        avatar.src = initialAvatarURL;
                        if (data == "refresh") {
                            setTimeout(function () { $alert.show().addClass('alert-danger').text("Yükleme işlemi başarıyla gerçekleşmiştir.") }, 200);
                            setTimeout(function () { window.location.reload(); }, 2000);
                        }
                    },

                    complete: function () {
                        $progress.hide();
                    },
                });
            });
        }
    });
});

var checkbox = $('input[name="selectCheckBox"]');
//Modal Trigger Button
var bZM = $(".zoomModalBtn");
var dMB = $(".deleteModalBtn");
var dMSBtn = $('#btnDeleteSelect');//Modal button for select all photos
var bD = $('#btnDelete');//Modal button for select all photos
var sBtn = $('#selectBtn');
var cBtn = $('#cancelBtn');
var dTBtn = $('#deleteTopBtn');
var alertTop = $('#alertTop');

dMSBtn.hide();
bD.hide();

bZM.on('click', function () {
    $('#zoomConfirmModal').modal('show');
    var filename_value = $(this).data('filename');
    $(".confirmImg").attr("src", "/upload/" + filename_value);
});
dMB.on('click', function () {
    bD.show();
    $('#deleteConfirmModal').modal('show');
    var filename_value = $(this).data('filename');
    $(".confirmImg").attr("src", "/upload/" + filename_value);
    bD.val(filename_value);
});
dTBtn.on('click', function () {
    dMSBtn.show();
    bD.hide();
    $('.confirmImg').hide();
    $('#deleteConfirmModal').modal('show');
});

//Delete Image With Checkbox
checkbox.hide();
dTBtn.hide();
alertTop.hide();
cBtn.hide();


sBtn.on("click", function () {
    checkbox.show();
    sBtn.hide();
    cBtn.show();
    dTBtn.show();

    dMSBtn.on("click", function () {
        $('#deleteConfirmModal').modal('hide');

        var filename = [];
        $('input[name="selectCheckBox"]:checked').each(function (i) {
            filename[i] = $(this).data("filename");
            console.log(filename[i]);

            var formData = new FormData();
            formData.append("filename", filename[i]);
            //Delete Ajax
            $.ajax({

                url: "/admin/media/deleteImage",
                type: "POST",
                processData: false,
                contentType: false,
                dataType: 'json',
                data: formData
            }).done(function (data) {
                checkbox.hide();
                cBtn.hide();
                sBtn.show();
                dTBtn.hide();
                if (data == "refresh") {
                    setTimeout(function () { alertTop.show().addClass('alert-success').text("Silme işlemi başarıyla gerçekleşmiştir.") }, 200);
                    setTimeout(function () { window.location.reload(); }, 2000);
                }
            }).fail(function () {
                setTimeout(function () { alertTop.show().addClass('alert-danger').text("Silme işlemnizi gerçekleştiremedik. Lütfen biraz sonra tekrar deneyiniz.") }, 200);
            });
        });
    });
});
cBtn.on('click', function () {
    checkbox.hide();
    cBtn.hide();
    sBtn.show();
});

//Delete Image in Modal
$(document).on('click', '#btnDelete', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.val();
    var formData = new FormData();
    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/deleteImage",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData,
        success: function (data) {
            if (data == "refresh") {
                window.location.reload(); // This is not jQuery but simple plain ol' JS
            }
        }
    }).done(function (data) {
        obj.closest('p').remove();
    });
});