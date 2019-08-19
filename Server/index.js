var io = require('socket.io')(process.env.PORT || 52300);

//customClasses
var Player = require('./Clases/Player.js');

console.log('Server has Started');

var players=[];
var sockets = [];

//los enventos On son eventos enviados del cliente -> server 
io.on('connection', function(socket){
    console.log('connection Made :D');

    var player = new Player();
    var thisPlayerID = player.id; //numero de indentificacion unico del jugador

    players[thisPlayerID] = player; // guardo el jugador en el arreglo de jugadores 
    sockets[thisPlayerID] = socket; //guardo la conecion que recivi en el areglo de coneciones 

    //le digo al cliente cual es nuestro ID en el server
    socket.emit('register' , {id:thisPlayerID}); //metodo que emite un evento de el server -> cliente  // envia la Data en formato JSON
    socket.emit('spawn', player); //me digo a mi mismo que apareci
    // puede recivir una clase dado que JSON significa Java Scrpit Object Notation, puede hacer el cast nativo 

    socket.broadcast.emit('spawn',player) //emite un mensaje a otodos los conectados menos a mi // se comunico a los demas que apareci

    //me digo a mi cliente acerca de quienes son los demas jagadores 
    for(var playerID in players)
    {
        if(playerID!= thisPlayerID)
            socket.emit('spawn', players[playerID]);
    }

    socket.on('updatePosition',function(data){

        player.position.x = data.position.x.value;
        player.position.y = data.position.y.value;
        player.position.z = data.position.z.value;
        
        socket.broadcast.emit('updatePosition',player);

        // var d ={         // crear pequeños Objetos JSON es otra forma mas optima de enviar informacion 
        //     id ="",
        //     position={       //se seleciona solo lo necesario 
        //         x=player.position.x,
        //         y=player.position.y
        //     }
        // }        //por el momento se va a estar enviando la clase completa 

        

    })

    socket.on('disconnect', function (){
        console.log('A Player has disconnected');
        delete players[thisPlayerID];//borro el jugador en el arreglo de jugadores 
        delete sockets[thisPlayerID];//borro la conexion en el arreglo de conexiones 

        socket.broadcast.emit('disconnected',player);

    });

});