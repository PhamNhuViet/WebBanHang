//hiển thị ảnh
const input = document.querySelector("[name='ImageUpload']");
const image = document.querySelector("#avatar");

if (input) {
    input.addEventListener("input", function (e) {
        if (e.target.files.length > 0) {
            const file = e.target.files[0];
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = function (e) {

                if (image) {
                    image.src = e.target.result;
                }
            }
        }
    })
}
//----------------------------------------------