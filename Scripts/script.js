$(document).ready(function () {
    // all td click event off until user selcevt  x or o 
    $('td').off('click');
});
var whoWin;
var computer;
var user;
var whoTurnIs = 'user';
var countTurns = 0;
var board = [];

function Win(player) {
    $.ajax({
        type: "POST",
        url: "/Home/Ajax",
        content: "application/json",
        dataType: "json",
        cache: false,
        data: { data: player },
        success: function (d) {
            console.log(d);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log("xhr = " + xhr + " textStatus = " + textStatus + " errorThrown = " + errorThrown)
        }
    });
}

board[0] = new Array(null, null, null);
board[1] = new Array(null, null, null);
board[2] = new Array(null, null, null);


$("#xPly").click(function () {
    $("#oPly").remove();
    $(".breakline").remove();
    user = 'x';
    computer = 'o';
    $('td').on("click", clickEvent);
    $("#xPly").off('click');
});

$("#oPly").click(function () {
    $("#xPly").remove();
    $(".breakline").remove();
    user = 'o';
    computer = 'x';
    $('td').on("click", clickEvent);
    $("#oPly").off('click');
});

function clickEvent() {
    countTurns++;
    countTurns == 9 ? location.reload() /*reolad page if there's not any move left*/ : null;

    if (whoTurnIs == 'user') {
        $(this).html(user);
        $(this).addClass(user);
        var cellNum = this.getAttribute('data-numcell');
        $('#' + cellNum).off('click');
        this.setAttribute('data-dirtyCell', true);
        whoTurnIs = 'computer';
        $('#playerTurn').html(whoTurnIs);
        //assign value to board-matrix cellNum / 3 = row , cellNum % 3 = Column number 
        board[Math.floor(cellNum / 3)][Math.floor(cellNum % 3)] = 'user';
        //check if win , if nt wait a seconde (more nice to play like this) and than pass the turn to the computer
        if (checkWin('user')) {
            setTimeout(function () {
                computerTurn();
            }, 1000);
        }
    } else {
        alert('something want wrong please refresh your browser');
        location.reload();
    }
}

function computerTurn() {

    countTurns++;
    if (countTurns == 9) {
        alert("Nobody win")
        location.reload()
    } else {
        null
    };
    // get the empty cell
    var leftCells = $('[data-dirtyCell=false]');
    // random number == random cell
    var cellLocation = Math.floor(Math.random() * leftCells.length);
    $(leftCells[cellLocation]).html(computer);
    //dodanie klasy css
    $(leftCells[cellLocation]).addClass(computer);
    // turn off click event on this cell
    $(leftCells[cellLocation]).off('click');
    var cellNum = leftCells[cellLocation].getAttribute('data-numcell');
    //asign true to cell that was clicked
    leftCells[cellLocation].setAttribute('data-dirtyCell', true);
    whoTurnIs = 'user';
    $('#playerTurn').html(whoTurnIs);
    //assign value to board-matrix cellNum / 3 = row , cellNum % 3 = Column number 
    board[Math.floor(cellNum / 3)][Math.floor(cellNum % 3)] = 'computer';
    checkWin('computer');
}

function checkWin(whoClick) {
    var searchRow;
    var searchColumn;
    var i, j, inc;
    i = 0;
    // check row and Column 
    for (i; i < 3; i++) {
        j = 0;
        inc = 0;
        searchRow = board[i][j];
        searchColumn = board[j][i];
        if (!searchRow && !searchColumn) {
            continue;
        }
        for (j = 0; j < 3; j++) {

            if ((searchRow && board[i][j] == searchRow && whoClick == searchRow) ||
                (searchColumn && board[j][i] == searchColumn && whoClick == searchColumn)) {
                inc++;
                if (inc == 3) {
                    whoWin = (searchRow ? searchRow : searchColumn);
                    //alert(whoWin + ' Win');

                    Win(whoWin);
                    location.reload();
                }
            } else {
                break;
            }
        }
    }
    //check First Diagonals [0,0][1,1][2,2]
    inc = 0;
    for (var i = 0; i < 3; i++) {
        if (board[i][i]) {
            if (i > 0 && (board[i][i] == board[i - 1][i - 1])) {
                inc++;
                if (inc == 2) {
                    whoWin = board[i][i];
                    //alert(whoWin + ' Win');
                    Win(whoWin);
                    location.reload();
                }
            }
        } else {
            var i = 0;
            var j = 2;
            //check seconde Diagonals [0,2][1,1][2,0]
            if (board[i][j]) {
                for (i, j; i < 3; i++ , j--) {
                    if (i > 0 && board[i][j] == board[i - 1][j + 1]) {
                        inc++;
                        if (inc == 2) {
                            whoWin = board[i][j];
                            //alert(board[i][j] + ' Win');
                            Win(whoWin);
                            location.reload();
                        }
                    }
                }
            }
            return true;
        }
    }
    return true;
}