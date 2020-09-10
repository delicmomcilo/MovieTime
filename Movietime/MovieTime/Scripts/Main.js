
//Transition effekt for navigasjonsbar
$(function () {
    $(window).scroll(function () {
        //Sjekker om vindu er skrollet mer enn 125px, legger til/ bakgrunnsfarge
        if ($(this).scrollTop() > 125) {
            $('.navbar').css('background-color', 'rgba(0,0,0,0.85)');
        } else {
            $('.navbar').css('background-color', 'transparent');
        }
    });
});



$(function () {
    $("#btnSubmit").hide();
    $("#btnEdit").click(function () {
        var inputs = document.getElementsByClassName('form-control');
        $("#btnEdit").hide();
        $("#btnSubmit").show();
        for (var i = 0; i < inputs.length; i++) {
            
            inputs[i].disabled = false;

        }
    });
});

//Funksjon for å sende data inn i popup
$(function () {

    $('#handlekurv').load('/Ordre/HentHandleKurv');
    $(document).on('click', 'a[data-filmModal=true]', function () {
          
        console.log("ÅPNET!");
        // hent inn data-taggene fra denne a-hef lenken
        var data_navn = $(this).data('navn');
        var data_id = $(this).data('id');
        var data_beskrivelse = $(this).data('beskrivelse');
        var data_pris = $(this).data('pris');
        var data_bilde = $(this).data('filmbilde');
        var data_sjanger = $(this).data('sjanger');
        // legg ut dataene i div'en i modal-view'et
        console.log(data_navn);
        $('#sjanger').html(data_sjanger);
        $('#navn').html(data_navn);
        $('#beskrivelse').html(data_beskrivelse);
        $('#pris').html(data_pris);
        $("#bilde").attr("src", data_bilde);


        $("#kjopModalKnapp").unbind().click(function () {
            // ID på film som sendes til LagOrdre i Ordre kontroller.
            $.ajax({
                type: "POST",
                url: '/Ordre/LagOrdre',
                data: { id: data_id },  // JSON objekt som inneholder id på fulm.
                success: function (data) {
                    if (data.fallBack != null) {
                        console.log(data.fallBack);

                        window.location.href = (data.fallBack);
                    }
                    
                    $('#handlekurv').load('/Ordre/HentHandleKurv');
                    $('#filmPopupModal').modal('hide');
                    $(".dropdown-toggle-handlekurv").toggle();
                }
            });

        })


    }) 
}); 



//Funksjon for å hente sjangre fra backend og vise filmene innenfor sjangeren
$(function () {
    //Sjekker om body inneholder FilmSide, sørger for at det ikke blir unødvendig ajax kall på andre sider.
    if ($('body').is('.FilmSide')) {
        HentAlleFilmer();
        HentAlleSjangre();

        // opprett en hendelse på dropdown-listen når siden lastes

        $("#dropDown").change(function () {
            var id = $(this).val();
            if (id == "-1") {
                HentAlleFilmer();
            } else {
                HentFilmerSjanger(id);
            }
        });
        function HentAlleSjangre() {
            $.ajax({
                url: '/Film/HentAlleSjangreJson',
                type: 'GET',
                dataType: 'json',
                success: function (jsSjanger) {
                    console.log("Hentet alle sjangre");
                    VisDropDown(jsSjanger);
                },
                error: function (x, y, z) {
                    alert(x + '\n' + y + '\n' + z);
                }
            });

        }

        function HentAlleFilmer() {
            $.ajax({
                url: '/Film/HentAlleFilmerJson',
                type: 'GET',
                dataType: 'json',
                success: function (filmer) {
                    VisFilmer(filmer);
                },
                error: function (x, y, z) {
                    alert(x + '\n' + y + '\n' + z);
                }
            });
        }

        function HentFilmerSjanger(id) {
            $.ajax({
                url: '/Film/HentFilmerSjangerJson/' + id,
                type: 'GET',
                dataType: 'json',
                success: function (filmer) {
                    VisFilmer(filmer);
                },
                error: function (x, y, z) {
                    alert(x + '\n' + y + '\n' + z);
                }
            });
        }

        function VisDropDown(jsSjanger) {
            var utStreng = "";
            for (var i in jsSjanger) {
                utStreng += "<option value='" + jsSjanger[i].ID + "'>" + jsSjanger[i].sjanger + "</option>";
            }
            $("#dropDown").append(utStreng);
        }

        function VisFilmer(filmer) {
            $('#filmer').empty();

            console.log(filmer);

            for (var i in filmer) {
                var utStreng = '<div class="card mb-4 mx-auto" style="min-width: 18rem; max-width: 18rem; margin-right: 10px" data-toggle="modal" data-target="#filmPopupModal">' +
                    '    <div class="card" style="width: 18rem;">' +
                    '        <a href="#filmPopupModal" data-filmModal="true" id="filmModal" data-navn="' + filmer[i].Filmnavn + '" data-id="' + filmer[i].ID + '" data-filmbilde="' + filmer[i].Filmbilde + '" data-beskrivelse="' + filmer[i].Beskrivelse + '" data-pris="' + filmer[i].Pris + '" data-sjanger="' + filmer[i].Sjanger + '" data-toggle="modal">' +
                    '            <img class="card-img-top" src="' + filmer[i].Filmbilde + '" alt="Card image cap">' +
                    '        </a>' +
                    '    </div>' +
                    '</div>';

                $("#filmer").append(utStreng);

            }

        }
    }


});
