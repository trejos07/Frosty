var io = require('socket.io')(process.env.PORT || 52300);

console.log('Server has Started');

io.on('connection', function(socket){
    console.log('connection Made :D');

    socket.on('disconnect', function (){
        console.log('A Player has disconnected');

    });

});