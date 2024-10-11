import { Tag } from './Tag.js'

export class TagClone extends Tag {
    constructor(data) {
        super(data)
        this.tagId = data.tagId
        this.keepId = data.keepId
        this.keepTagId = data.keepTagId
    }
}