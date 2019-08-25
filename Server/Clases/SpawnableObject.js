var Vector3 = require('./Vector3.js');
var ShortID = require('shortid');

module.exports= class SpawnableObject{

    constructor(){
        this.id = ShortID.generate();
        this.name = 'ServerObject';
        this.position = new Vector3();
    }
};