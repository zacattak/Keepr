<template>

    <section class="row d-flex justify-content-center">
        <div @click="getKeepById()" class="selectable" type="button" data-bs-toggle="modal" data-bs-target="#keepModal">
            <p class="mb-0 text-center">Views:{{ keep.views }} Kept:{{ keep.kept }}</p>
            <h2 class="text-center">{{ keep.name }}</h2>
            <img :src="keep.img" :alt="keep.name" class="img">
        </div>
        <!-- <div v-if="keep.creatorId == account.id">

            <button @click="deleteKeep(keepId)" type="button" class="btn btn-primary">DELETE</button>
        </div> -->

    </section>

</template>


<script>
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
export default {
    props: {
        keep: { type: Keep, required: true }
    },
    setup(props) {
        return {

            getKeepById() {
                try {
                    keepsService.getKeepById(props.keep.id)
                }
                catch (error) {
                    Pop.error(error);
                }
            }
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
</style>