module.exports = class Vector3{
    constructor(x=0,y=0,z=0){
        this.x = x;
        this.y = y;
        this.z = z
    }

    Magnitude(){
        return Math.sqrt((this.x**2) + (this.y**2)+ (this.z**2));
    }

    Normalized()
    {
        var mag = this.Magnitude();
        return new Vector3(this.x/mag,this.y/mag,this.z/mag);
    }

    Distance(OtherVector = Vector3){
        var dir = new Vector3();
        dir.x = OtherVector.x-this.x;
        dir.y = OtherVector.y-this.y;
        dir.z= OtherVector.z=this.z;
        return dir.Magnitude();
    }

    ConsoleOutput()
    {
        return '('+this.x +','+this.y+','+this.z+')';
    }
}