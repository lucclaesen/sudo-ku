
$(function () {

    // Hook up the changed event: fires when the value of an input element is changed and the input looses focus.
    // Note that jquery implicitly iterates over the selection result. Thus, the result is equivalent to
    // $(".sudo-grid input").each(change(function{...}));
    $(".sudo-grid input").change(function (event) {
        // 'this' refers to the dom element that fires the change event
        // by assigning it to a local var, this dom element becomes available for closures of nested functions.
        var inputElement = this;


        // If the current cell previously invalidated and the current change consists in emptying the cell, do not make a validate call;
        // simply remove the invalidation class
        if ($(inputElement).hasClass("invalid")) {
            if (inputElement.value == "") {
                $(inputElement).removeClass("invalid");
                return;
            }
        }
        
        // prevent input if another cell is invalid
        var invalidCells = $(".sudo-grid input[class=invalid]:not([data-seq=" + $(inputElement).data("seq") + "])");
        if (invalidCells.length > 0) {
            inputElement.value = "";
            return;
        }

        var validateActionUrl = $(".sudo-grid").data("validateurl");
        var data = {
            values: getPuzzleData()
        };
        data = JSON.stringify(data);

        // make a get call to validate, sending along the whole puzzle data
        $.ajax({
            url: validateActionUrl,
            data: data,
            type: "POST", /* I wanted a get here, but mvc5 with it's default parameter binder does not handle JSon data well*/
            dataType: "json",
            contentType: 'application/json', /*this is crucial*/
            success: function (json) {
                // code that runs of no errors were encountered
                $(".sudo-grid input").each(function () {
                    // remove validation colors from all cells other than the one that just fired
                    $(this).removeClass("valid invalid");
                    if ($(this).data("seq") == $(inputElement).data("seq")) {
                        if (json.valid) {
                            $(inputElement).addClass("valid");
                            $(inputElement).prop("readonly", true);                            
                        }
                        else {
                            $(inputElement).addClass("invalid");
                        }
                    }
                   
                })
            },
            error: function (xhrResult, status, json) {
                // code that runs in case of error
            },
            complete: function (xhrResult, json) {
                // code that executes in both success and error scenario's
            }
        });
        
    });

    //$(document).keydown(function (e) {
    //    // If focus is on an input cell, up down, left and right arrays should nevigate over the grid
    //    var focusCell = document.activeElement;
    //    var activeIndex = $(focusCell).data("seq");
    //    var newIndex = 0;
    //    if ($(focusCell).is(".sudo-grid input")){
    //        switch (e.which) {
    //            case 37:    // left
    //                newIndex = activeIndex == 0 ? activeIndex : activeIndex - 1;
    //                break;
    //            case 38:    //up
    //                newIndex = activeIndex / 9 ==  0 ? activeIndex : ((activeIndex / 9) - 1) + (activeIndex % 9);
    //                break;
    //            case 39:    // right
    //                newIndex = activeIndex == 80 ? activeIndex : activeIndex + 1;
    //                break;
    //            case 40:    // down
    //                newIndex = activeIndex / 9 == 8 ? activeIndex : ((activeIndex / 9) + 1) + (activeIndex % 9);
    //                break;
    //        }
    //        $(".sudo-grid input[seq='newIndex']").focus();
    //    }
    //});

    function getPuzzleData() {
        // first, we need an array of the input dom elements, then sort them by orderId [0..80]
        var inputElements = $(".sudo-grid input")
            .sort(function (a, b) {
                return $(a).data("seq") - $(b).data("seq");
            });

        // create and return an array of their values. Note that we expect a byte[] at the server side. 
        var values = [];
        inputElements.each(function ()
        {
            var numericValue = Number(this.value == "" ? "0" : this.value);
            values.push(numericValue);
        });
        return values;
    }
});