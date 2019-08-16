var io = require('socket.io')(process.env.PORT || 52300);

//customClasses
var Player = require('./Clases/Player.js');

console.log('Server has Started');

var players=[];

io.on('connection', function(socket){
    console.log('connection Made :D');

    socket.on('disconnect', function (){
        console.log('A Player has disconnected');

    

    });

});