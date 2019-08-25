var io = require('socket.io')(process.env.PORT || 52300);

//customClasses
var Player = require('./Clases/Player.js');
var Bullet = require('./Clases/Bullet.js');

console.log('Server has Started');

var players=[];
var sockets = [];
var bullets =[];

//updates
setInterval(()=>{
    bullets.forEach(bullet => {
        var isDestroyed = bullet.onUpdate();

        if(isDestroyed){ 
            var index = bullets.indexOf(bullet);
            if(index>-1){
                bullets.splice(index,1);

                let returnData ={
                    id: bullet.id
                };

                for(var playerID in players){
                    sockets[playerID].emit('serverUnspawn',returnData);
                }
            }
        } else{

            let returnData ={
                id: bullet.id,
                position: {
                    x:bullet.position.x.toString(),
                    y:bullet.position.y.toString(),
                    z:bullet.position.z.toString()
                },
            };
            
            for(var playerID in players){
                sockets[playerID].emit('updatePosition',returnData);
            }
        }

    });
},100,0);

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

    socket.broadcast.emit('spawn',player); //emite un mensaje a otodos los conectados menos a mi // se comunico a los demas que apareci

    //me digo a mi cliente acerca de quienes son los demas jagadores 
    for(var playerID in players)
    {
        if(playerID!= thisPlayerID)
            socket.emit('spawn', players[playerID]);
    }

    socket.on('disconnect', function (){
        console.log('A Player has disconnected');
        delete players[thisPlayerID];//borro el jugador en el arreglo de jugadores 
        delete sockets[thisPlayerID];//borro la conexion en el arreglo de conexiones 

        socket.broadcast.emit('disconnected',player);

    });

    socket.on('updatePosition',function(data){//cuando me mueva 

        //console.log(data.position._x);
        player.position.x = parseFloat(data.position._x);//actualizo mi representacion en el server
        player.position.y = parseFloat(data.position._y);
        player.position.z = parseFloat(data.position._z);
        
        //console.log(player.position.ConsoleOutput());

        var d ={         // crear pequeños Objetos JSON es otra forma mas optima de enviar informacion 
            id :player.id,
            position:{       //se seleciona solo lo necesario 
                x:player.position.x.toString(),
                y:player.position.y.toString(),
                z:player.position.z.toString()
            }
        };
        //console.log('updatePosition Send',d);
        socket.broadcast.emit('updatePosition',d);// le digo a los demas que me movi 

    });

    socket.on('updateRotation',function(data){
        player.rotation.x = parseFloat(data._x);
        player.rotation.y = parseFloat(data._y);
        player.rotation.z = parseFloat(data._z);

        var d ={       
            id :player.id,
            rotation:{       
                x:player.rotation.x.toString(),
                y:player.rotation.y.toString(),
                z:player.rotation.z.toString()
            }
        };

        socket.broadcast.emit('updateRotation',d);// le digo a los demas que me movi 

    });

    socket.on('fireBullet',function(data){
        var bullet = new Bullet();
        bullet.name = 'Bullet';

        bullet.position.x = parseFloat(data.position._x);
        bullet.position.y = parseFloat(data.position._y);
        bullet.position.z = parseFloat(data.position._z);

        bullet.direction.x = parseFloat(data.direction._x);
        bullet.direction.y = parseFloat(data.direction._y);
        bullet.direction.z = parseFloat(data.direction._z);
        bullets.push(bullet);
        //console.log(bullet);
        var returnData = {
            name: bullet.name,
            id: bullet.id,
            position:{
                x: bullet.position.x.toString(),
                y: bullet.position.y.toString(),
                z: bullet.position.z.toString()
            },
            direction:{
                x:bullet.direction.x.toString(),
                y:bullet.direction.y.toString(),
                z:bullet.direction.z.toString()
            }
        };

        //console.log(returnData);
        socket.emit('serverSpawn', returnData);
        socket.broadcast.emit('serverSpawn', returnData);

    });

});


function interval(func, wait, times){

    var interv = function(w,t){
        return function(){
            if(typeof t === "undefined"|| t-- >0){
                setTimeout(interv,w);
                try{
                    func.call(null);
                }catch(e){
                    t=0;
                    throw e.toString();
                }
            }
        };
    }(wait ,times);

    setTimeout(interv,wait);
}