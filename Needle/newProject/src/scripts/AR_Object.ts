import { Behaviour, Camera, GameObject, serializable, setActive } from "@needle-tools/engine";
import { Quaternion, Vector3 } from "three";
import { degToRad } from "three/src/math/MathUtils";

export class ar_Object extends Behaviour {
    @serializable(GameObject)
    panels?: GameObject[];

    @serializable(GameObject)
    ui?: GameObject;

    @serializable(Camera)
    ar_Cam?: Camera;

    toggle: boolean = true;

    start() {
 
        this.togglePanels();

        console.log(this.gameObject.name + " => (ar_cam_ref) : " + this.ar_Cam);

    }

    update(): void {
        if (!this.ar_Cam || !this.ui) return;
    
        // Get the camera's position and the UI's position
        const cameraPosition = this.ar_Cam.worldPosition;
        const uiPosition = this.ui.worldPosition;
    
        // Calculate the direction vector from the UI to the camera, ignoring the Y-axis
        const direction = cameraPosition.clone().sub(uiPosition);
        direction.y = 0; // Lock rotation on the Y-axis (horizontal plane)
    
        // If direction is not zero, make the UI look at the target position
        if (direction.lengthSq() > 0) {
            this.ui.lookAt(uiPosition.clone().add(direction));
        }
    
        this.ui.rotateY(degToRad(180));
    }
    

    public togglePanels(): void {
        this.toggle = !this.toggle;

        if (this.panels) {
            this.panels.forEach(element => {
                setActive(element, this.toggle);
            });
        }
    }

}