var shortID = require('shortid');
var Vector3 = require('./Vector3.js')

module.exports = class Player{
    constructor(){
        this.username ='';
        this.id = shortID.generate();
        this.position = new Vector3();
    }

}