$(function () {

    /*
     * Marca visualmente la opción activa del menú.
     * Uso real de jQuery en la navegación.
     */

    var currentPath = window.location.pathname.toLowerCase();

    $(".app-header .nav-link").each(function () {

        var linkPath = $(this)
            .attr("href");

        if (!linkPath) {
            return;
        }

        linkPath = linkPath.toLowerCase();

        if (
            currentPath === linkPath ||
            currentPath.startsWith(linkPath + "/")
        ) {
            $(this).addClass("active");
        }

    });


    /*
     * Micro-interacción de botones.
     */

    $(".btn").on("mouseenter", function () {

        $(this).css(
            "box-shadow",
            "0 4px 12px rgba(15, 23, 42, 0.10)"
        );

    });

    $(".btn").on("mouseleave", function () {

        $(this).css(
            "box-shadow",
            ""
        );

    });

});

/*
 * Filtro de movimientos del dashboard.
 *
 * Permite mostrar todos los movimientos,
 * únicamente compras o únicamente pagos.
 *
 * Implementado con jQuery.
 */
$(function () {

    $(".transaction-filter").on("click", function () {

        var filter = $(this).data("filter");

        $(".transaction-filter")
            .removeClass("active");

        $(this)
            .addClass("active");


        var visibleRows = 0;

        $(".transaction-row").each(function () {

            var transactionType = $(this).data("type");

            if (
                filter === "ALL" ||
                transactionType === filter
            ) {

                $(this).show();

                visibleRows++;

            }
            else {

                $(this).hide();

            }

        });


        if (visibleRows === 0) {

            $("#transactionsTable")
                .hide();

            $("#noFilteredTransactions")
                .show();

        }
        else {

            $("#transactionsTable")
                .show();

            $("#noFilteredTransactions")
                .hide();

        }

    });

});

/*
* Mostrar / ocultar número completo de tarjeta.
*/
$(function () {

    var cardNumberVisible = false;

    $("#toggleCardNumber").on("click", function () {

        if (cardNumberVisible) {

            var maskedNumber =
                $("#cardNumber").data("masked");

            $("#cardNumber")
                .html(maskedNumber);

            $("#eyeOpenIcon")
                .hide();

            $("#eyeClosedIcon")
                .show();

            $("#toggleCardNumber")
                .attr(
                    "aria-label",
                    "Mostrar número de tarjeta"
                )
                .attr(
                    "title",
                    "Mostrar número de tarjeta"
                );

            cardNumberVisible = false;

            return;
        }


        var confirmModal =
            new bootstrap.Modal(
                document.getElementById(
                    "confirmCardNumberModal"
                )
            );

        confirmModal.show();

    });


    $("#confirmShowCardNumber").on("click", function () {

        var fullNumber =
            $("#cardNumber").data("full");

        var formattedNumber =
            fullNumber
                .toString()
                .replace(
                    /(.{4})/g,
                    "$1 "
                )
                .trim();

        $("#cardNumber")
            .text(formattedNumber);

        $("#eyeClosedIcon")
            .hide();

        $("#eyeOpenIcon")
            .show();

        $("#toggleCardNumber")
            .attr(
                "aria-label",
                "Ocultar número de tarjeta"
            )
            .attr(
                "title",
                "Ocultar número de tarjeta"
            );

        cardNumberVisible = true;


        var modalElement =
            document.getElementById(
                "confirmCardNumberModal"
            );

        var modal =
            bootstrap.Modal.getInstance(
                modalElement
            );

        modal.hide();

    });

});