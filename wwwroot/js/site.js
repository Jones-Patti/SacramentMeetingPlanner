

   function runUpdate(talk1, talk2){
        $.post("/Speaker/SwitchSpeakers",{

            talk1: talk1,
            talk2: talk2

            }, function(data){

                if(data != 0){
                    updateView(data);
                } else {

                    alert("There as an error updating the records");

                }
               

        });
   }

   function updateView(data){

        $("#speaker_area").html("<div class=\"draggable\"><div> Speaker </div><div> Topic </div> </div>");


        var obj = JSON.parse(data);

        for (var i = 0; i<obj.Speakers.length; i++) {

            var tmpHtml = ('<div class="draggable">')
                tmpHtml = tmpHtml.concat('<input type="hidden" value=' +  obj.Speakers[i].SpeakerId   + '>');
                tmpHtml = tmpHtml.concat('<div>' + obj.Speakers[i].People.FirstName + ' ' + obj.Speakers[i].People.LastName + '</div>');
                tmpHtml = tmpHtml.concat('<div>' + obj.Speakers[i].Topic.TopicTitle + '</div>');
                tmpHtml = tmpHtml.concat('<div><a href = "/Speaker/Edit' + obj.Speakers[i].SpeakerId + '"> Edit </a> |');
                tmpHtml = tmpHtml.concat('<a href = "/Speaker/Details' + obj.Speakers[i].SpeakerId + '"> Details </a> |');
                tmpHtml = tmpHtml.concat('<a href = "/Speaker/Delete' + obj.Speakers[i].SpeakerId + '"> Delete </a> </div>');
               
                $("#speaker_area").append(tmpHtml);
        }

        var tmpHtml = ("<script src=\"/js/draggableAndDropAble.js\"></script>");
         $("#speaker_area").append(tmpHtml);
   }

   function createSpeaker(){

    $(".loader").show();

        $.post("/Speaker/CreateSpeaker",{

            PeopleId: $("#Speaker_PeopleId").val(),
            SacramentId: $("#Speaker_SacramentId").val(),
            TopicId: $("#Speaker_TopicId").val()

            }, function(data){

               $(".loader").hide();

                $('#exampleModalCenter').hide();

                $('body').removeClass('modal-open'); 
                $('.modal-backdrop').remove();
                if(data != 0){
                    

                        $('#exampleModalCenter').hide();

                        $('body').removeClass('modal-open'); 
                        $('.modal-backdrop').remove();
                    updateView(data);

                } else {

                    alert("There as an error Adding Speaker");
                     

                }
               

        });



   }

