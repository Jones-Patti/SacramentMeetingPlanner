

   function runUpdate(talk1, talk2){

        $(".loader").show();
        $.post("/Speaker/SwitchSpeakers",{

            talk1: talk1,
            talk2: talk2

            }, function(data){

                if(data != 0){
                    $(".loader").hide();
                    updateView(data);

                } else {

                    alert("There as an error updating the records");

                }
               

        });
   }

   function updateView(data){

    
        $("#speaker_area").html('<div class="loader"></div>');
        $("#speaker_area").append("<div class=\"draggableHeader\"><div> Speaker </div><div> Topic </div> </div>");

      


        var obj = JSON.parse(data);

        for (var i = 0; i<obj.Speakers.length; i++) {

            var tmpHtml = ('<div class="draggable">')
                tmpHtml = tmpHtml.concat('<input type="hidden" value=' +  obj.Speakers[i].SpeakerId   + '>');
                tmpHtml = tmpHtml.concat('<div>' + obj.Speakers[i].People.FirstName + ' ' + obj.Speakers[i].People.LastName + '</div>');
                tmpHtml = tmpHtml.concat('<div>' + obj.Speakers[i].Topic.TopicTitle + '</div>');
                tmpHtml = tmpHtml.concat('<div><a href = "/Speaker/Edit' + obj.Speakers[i].SpeakerId + '"> Edit </a> |');
                //tmpHtml = tmpHtml.concat('<a href = "/Speaker/Details' + obj.Speakers[i].SpeakerId + '"> Details </a> |');
                tmpHtml = tmpHtml.concat('<a href="#" data-toggle="modal" data-target="#modelDeleteConfirm" onclick="confirmDelete(');
                tmpHtml = tmpHtml.concat("'" + obj.Speakers[i].SpeakerId + "', '" + obj.Speakers[i].People.FirstName + "', '" + obj.Speakers[i].People.LastName + "', '" + obj.Speakers[i].Topic.TopicTitle + "')");
                tmpHtml = tmpHtml.concat('"> Delete</a></div>');


                $("#speaker_area").append(tmpHtml);
        }


        var tmpHtml = ("<script src=\"/js/draggableAndDropAble.js\"></script>");
         $("#speaker_area").append(tmpHtml);
   }

   function createSpeaker(){

    $("#addSpeaker_loader").show();

        $.post("/Speaker/CreateSpeaker",{

            PeopleId: $("#Speaker_PeopleId").val(),
            SacramentId: $("#Speaker_SacramentId").val(),
            TopicId: $("#Speaker_TopicId").val()

            }, function(data){

                $("#addSpeaker_loader").hide();

                $('#addSpeakerModal').hide();

                $('body').removeClass('modal-open'); 
                $('.modal-backdrop').remove();
                if(data != 0){
                 
                    updateView(data);

                } else {

                    alert("There as an error Adding Speaker");

                }

        });



   }

   var del_speaker_id = 0;

   function confirmDelete(speakerId, speakerFirstName, speakerLastName, speakerTopic){

        $("#delete_speaker_name").html(speakerFirstName + " " + speakerLastName);
        $("#delete_speaker_topic").html(speakerTopic);
        del_speaker_id = speakerId;
                
   }

   function deleteSpeaker(){

       
        $("#delete_speaker_loader").show();

        $.post("/Speaker/RemoveConfirmed",{

            id: del_speaker_id

            }, function(data){

               $("#delete_speaker_loader").hide();

                $('#modelDeleteConfirm').hide();
                $('body').removeClass('modal-open'); 
                $('.modal-backdrop').remove();

                if(data != 0){
                        updateView(data);
                } else {
                    alert("There as an error Removing Speaker");
                }
               

        });

   }

   function setEditModelValues(){

         $('#edit_speaker_id').val("31");

   }