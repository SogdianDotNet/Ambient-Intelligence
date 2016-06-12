var DisplayError = {
    ShowError: function () {
        $(function () {
            $(".uploadSubmitButton").click(function (e) {
                e.preventDefault;
                $.post("/Download/Upload", function (data) {
                    if (data.Success == true) {
                        alert("Success");
                        if (data.Object != null) {
                            //here I could call the properties of my object, as below:
                            alert(data.Object.Name); //assuming your object has a property name
                        }
                    }
                    else { alert(data.ErrorMessage); }
                });
                return false;
            });
        });
    }
};