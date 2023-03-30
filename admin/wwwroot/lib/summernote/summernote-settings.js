var mediaPathSetting = "/uploads/";
var summernoteSettings = {
    height: 250,
    placeholder: '...',
    colors: [["#000000", "#ffffff", "#1f3960", "#8a8d8f", "#FFDA00", "#7F4A20", "#E5F1FB", "#E81123", "#40568D", "#6CE26C","#1D7C8C"]],
    toolbar: [
        ['style', ['style', 'bold', 'italic', 'underline']],
        ['color', ['color']],
        ['para', ['ul', 'ol', 'paragraph']],
        ['image', ['link','picture', 'video', 'table']],
        ['misc', ['undo', 'redo', 'codeview']]
    ],
    callbacks: {
        onImageUpload: function (files) {
            //var $this = $(this);

            //console.log($this);

            //$this.summernote("insertImage", "https://www.logo.com.tr/img/logo.png");

            uploadContentToApiV2(files, $(this));
            
        }
    },
    dialogsInBody: true,
    lang: 'tr-TR'
}

var uploadContentToApiV2 = function (files, editor) {
   

    var formData = new FormData();
    formData.append("file", files[0]);

    return $.ajax({
        type: "POST",
        url: "/media/insert",
        data: formData,
        cache: false,
        processData: false,
        contentType: false,
        success: function (data) {
            var img = document.createElement('img');

            console.log(data);

            img.src = mediaPathSetting + data.newFileName;
            img.classList = "content-image";
            editor.summernote("insertNode", img);
        }
    });
}