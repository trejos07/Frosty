var ServerObject = require('./SpawnableObject.js');
var Vector3 = require('./Vector3');

module.exports=class Bullet extends ServerObject{

    constructor(){
        super();
        this.direction = new Vector3();
        this.speed =0.05;
    }

    onUpdate(){
        this.position.x += this.direction.x *this.speed;
        this.position.y += this.direction.y *this.speed;
        this.position.z += this.direction.z* this.speed;

        return false;
    }
};