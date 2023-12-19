
    document.querySelectorAll(".editButton").forEach(function (button) {
        button.addEventListener("click", function () {
            var id = this.getAttribute("data-id");
            document.getElementById("Id").value = id;
        });
        });
