namespace FileHelperLibrary.Web.Components.Templates
{
    public class CtrlFormModelTemplate
    {
        public static string Html()
        {
            return @"<form id='upload-file-form' action='/-#controller-/-#action-' enctype='multipart/form-data' method='post'>
                        <label for='input-file' class='text-center label-for-input-file'>-#label-text-</label>
                        <input type='file' id='input-file' name='file' accept='-#allowed-files-extension-'/>

                        <div class='upload-image-container'>
                            <img src='' id='image-to-upload' alt='-#img-title-'/>
                        </div>

                        <input type='submit' id='btn-upload-image' class='btn btn-primary btn-sm' value='-#btn-value-' disabled/>
                    </form>";
        }

        public static string Style()
        {
            return @"<style>
                        .label-for-input-file {
                            padding: 10px;
                            width: 100%;
                            max-width: 320px;
                            background: #222222;
                            color: #fff;}
                        #input-file {display: none;}
                        .upload-image-container { 320px; margin: 5px 0}
                        .upload-image-container img {width: 100%;}
                    </style>";
        }

        public static string Logic()
        {
            return @"<script src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.3.1.min.js'></script>
                    <script>
                        $('#input-file').change(function () {
                            var inputFile = $(this);
                                hideErrorMessage();
                                if ($(inputFile).get(0).files.length > 0) {
                                    if (isAllowedExtension(getExtension($(inputFile).val()))){
                                        if ($(inputFile).get(0).files[0].size <= -#max-file-size-) {
                                            var reader = new FileReader();
                                            reader.onload = function(e) { $('#image-to-upload').attr('src', e.target.result); };
                                            reader.readAsDataURL($(this).get(0).files[0]);
                                             $('#btn-upload-image').prop('disabled', false);
                                        }
                                        else{
                                            showErrorMessage('-#msg-for-max-size-'); removeImageToUpload();
                                        }
                                    }else{
                                        showErrorMessage('-#msg-for-extension-not-allowed-'); removeImageToUpload();
                                    }
                               }
                        });
                        function removeImageToUpload(){
                            $('#image-to-upload').attr('src', '');
                            $('#btn-upload-image').prop('disabled', true);
                        }
                        function hideErrorMessage(){
                            if (!$('#-#error-message-id-').hasClass('hidden')) { $('#-#error-message-id-').addClass('hidden'); }
                        }
                        function showErrorMessage(message){
                            $('#-#error-message-id-').text(message);
                            if ($('#-#error-message-id-').hasClass('hidden')) { $('#-#error-message-id-').removeClass('hidden'); }
                        }
                        function isAllowedExtension(extension){
                            var error = true;
                            var allowedExtension = [-#files-extension-for-js-];
                            if (!allowedExtension.includes(extension)) { error = false; }
                            return error;
                        }
                        function getExtension(path){
                            var indexOf = path.lastIndexOf('.');
                            return path.substr(indexOf, path.length).toLowerCase();
                        }
                    </script>";
        }
    }
}
