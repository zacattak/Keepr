import { AppState } from "../AppState.js";

import { Tag } from "../models/Tag.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class TagsService {
    async createTag(tagData) {
        const response = await api.post('api/tags', tagData)
        logger.log('created tag', response.data)
        const tag = new Tag(response.data)
        return tag
    }
    // async getAllTags() {
    //     const response = await api.get('api/tags')
    //     AppState.tags = response.data.map(t => new Tag(t))
    //     logger.log('fetched tags', AppState.tags)
    // }

    // async createTagIfUnique(tagData) {
    //     // Normalize the input tag name to lowercase
    //     const normalizedTagName = tagData.name.toLowerCase()

    //     // Fetch existing tags if not already in AppState
    //     if (!AppState.tags || AppState.tags.length === 0) {
    //         await this.getAllTags()
    //     }

    //     // Check if the tag already exists
    //     const existingTag = AppState.tags.find(tag => tag.name.toLowerCase() === normalizedTagName)

    //     if (existingTag) {
    //         logger.log('Tag already exists:', existingTag)
    //         return existingTag  // Return existing tag to avoid duplicate creation
    //     }

    //     // If tag does not exist, create a new one
    //     const response = await api.post('api/tags', tagData)
    //     const newTag = new Tag(response.data)
    //     AppState.tags.push(newTag)  // Optionally add it to the AppState if necessary
    //     logger.log('created new tag', newTag)
    //     return newTag
    // }
}

export const tagsService = new TagsService