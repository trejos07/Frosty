var Vector3 = require('./Vector3.js');

module.exports = class Grid{

    constructor(){
        
        this.originPos = new Vector3();
        this.cellSize = 0.1;
        this.gridSize = [250,250];
        this.cells = new Array(this.gridSize.x);

        for (var i = 0; i < x.length; i++) {
            this.cells[i] = new Array(this.gridSize.y);
        }
    }

    GridPosToWorldPos(_gPos)  {
        var x = this.originPos.x + _gPos.x * this.cellSize +this.cellSize / 2;
        var y = this.originPos.y + _gPos.y * this.cellSize +this.cellSize / 2;
        return new Vector3(x,0,y);
    }

    WorldToGridPos(_wPos)  {
        var x = ((_wPos.x - originPos.x) - this.cellSize / 2)/this.cellSize;
        var y = ((_wPos.y - originPos.y) - this.cellSize / 2)/this.cellSize;
        return [x,y];
    }

};

