    var draggableID = 0;

    $( ".draggable" ).draggable(
    {
        drag: function() {
            draggableID = $(this).find("input").val();
        }
    });

    $( ".draggable" ).droppable({
      drop: function( event, ui ) {
        
        var droppedID = $(this).find("input").val();
        runUpdate(droppedID, draggableID);
        }
    });