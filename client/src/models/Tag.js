export class Tag {
    constructor(data) {
        this.id = data.id

        this.name = data.name

        this.creatorId = data.creatorId
        this.creator = data.creator
    }
}