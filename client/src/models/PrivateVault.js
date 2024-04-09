export class PrivateVault {
    constructor(data) {
        this.id = data.id

        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.isPrivate = true

        this.creatorId = data.creatorId
        this.creator = data.creator
    }
}