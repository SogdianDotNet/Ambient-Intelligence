var JSONParse = {
    GetSession: function (data) {
        var json = JSON.parse(data);
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: "/Session/StartSession",
            data: json,
            cache: false,
            succes: function (json) {
                console.log(json + "Succes");
                JSONParse.StartSession(json);
            },
            error: function (json) {
                console.log("WRONG" + json);
            }
        });
    },
    StartSession: function (data) {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            url: "http://145.24.222.180:80/api/Session",
            data: data,
            cache: false,
            succes: function (data) {
                console.log(data);
                console.log("Json post was success" + data);
            },
            error: function (data) {
                console.log("Json was not succeeded." + data);
            }
        });
    }
};