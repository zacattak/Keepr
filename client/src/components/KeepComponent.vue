<template>

    <section class="row d-flex justify-content-center">
        <!-- <div @click="getKeepById()" class="selectable" type="button" data-bs-toggle="modal" data-bs-target="#keepModal">

            <h2 class="text-center">{{ keep.name }}</h2>

            <img :src="keep.img" :alt="keep.name" class="img-fluid rounded shadow">
        </div>
        <div v-if="keep.creatorId == account.id">

            <button @click="deleteKeep(keep.id)" type="button" class="btn btn-primary mt-1">DELETE</button>
        </div> -->
        <div @click="getKeepById()" class="selectable" type="button" data-bs-toggle="modal" data-bs-target="#keepModal">
            <h2 class="text-center">{{ keep.name }}</h2>
            <div class="image-container">
                <img :src="keep.img" :alt="keep.name" class="img-fluid rounded shadow">
                <div v-if="keep.creatorId == account.id" class="delete-button-container">
                    <button @click="deleteKeep(keep.id)" type="button" class="btn btn-primary">DELETE</button>
                </div>
            </div>
        </div>

        <div v-if="keepTags.length">
            <!-- <h3>Tags:</h3> -->
            <!-- <ul>
                <li v-for="keepTag in keepTags" :key="keepTag.id">{{ keepTag.name }}</li>
            </ul> -->
            <ul class="tags-container">
                <li v-for="keepTag in keepTags" :key="keepTag.id" class="tag-item">
                    {{ keepTag.name }}
                </li>
            </ul>
        </div>
    </section>



</template>


<script>
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState';
import { keepTagsService } from '../services/KeepTagsService.js';
// import { useRouter } from 'vue-router';
export default {
    props: {
        keep: { type: Keep, required: true }
    },
    setup(props) {



        async function getKeepTagsByKeepId() {
            try {
                console.log('Fetching tags for keep:', props.keep.id);
                await keepTagsService.getKeepTagsByKeepId(props.keep.id)
            }
            catch (error) {
                Pop.error(error);
            }
        }





        onMounted(getKeepTagsByKeepId)
        return {

            getKeepById() {
                try {
                    keepsService.getKeepById(props.keep.id)
                }
                catch (error) {
                    Pop.error(error);
                }
            },



            async deleteKeep(keepId) {
                try {
                    const yes = await Pop.confirm()
                    if (!yes) return

                    await keepsService.deleteKeep(keepId)
                    // router.push({ name: 'Home' })
                }
                catch (error) {
                    Pop.error(error);
                }
            },

            account: computed(() => AppState.account),
            // keepTags: computed(() => AppState.keepTags),
            keepTags: computed(() => AppState.keepTagsByKeep[props.keep.id] || []),
            // keepTags: computed(() => AppState.keepTags.filter(tag => tag.keepId === props.keep.id))
        }
    }
}
</script>


<style lang="scss" scoped>
.img {
    width: 100%;
    object-fit: cover;
    height: 40vh;
}

.image-container {
    position: relative;
    /* Set the parent to relative for positioning children */
}

.delete-button-container {
    position: absolute;
    /* Position the button absolutely within the relative container */
    bottom: 10px;
    /* Adjust as needed for vertical spacing from the bottom */
    left: 10px;
    /* Adjust as needed for horizontal spacing from the left */
    z-index: 10;
    /* Ensure the button is above the image */
}

.tags-container {
    display: flex;
    /* Display items in a row */
    flex-wrap: wrap;
    /* Allow wrapping to the next line if there are too many tags */
    list-style-type: none;
    /* Remove bullet points */
    padding: 0;
    margin: 0;
}

.tag-item {
    margin: 5px;
    /* Add spacing between tags */
    padding: 8px 12px;
    /* Add padding inside the tag */
    border: 1px solid #ccc;
    /* Add a rectangular border */
    border-radius: 8px;
    /* Round the corners */
    background-color: #f9f9f9;
    /* Optional background color */
    font-size: 14px;
    /* Font size for the tags */
}
</style>