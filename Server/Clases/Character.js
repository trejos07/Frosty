var shortID = require('shortid');
var Vector3 = require('./Vector3.js')

module.exports = class Character{
    constructor(){
        this.name ='';
        this.id = shortID.generate();
        this.position = new Vector3();
        this.rotation = new Vector3();

    }

}