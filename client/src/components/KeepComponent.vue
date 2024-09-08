<template>

    <section class="row d-flex justify-content-center">
        <div @click="getKeepById()" class="selectable" type="button" data-bs-toggle="modal" data-bs-target="#keepModal">
            <p class="mb-0 text-center">Views:{{ keep.views }} Kept:{{ keep.kept }}</p>
            <h2 class="text-center">{{ keep.name }}</h2>
            <img :src="keep.img" :alt="keep.name" class="img">
        </div>
        <div v-if="keep.creatorId == account.id">

            <button @click="deleteKeep(keep.id)" type="button" class="btn btn-primary">DELETE</button>
        </div>

    </section>

</template>


<script>
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import { computed } from 'vue';
import { AppState } from '../AppState';
// import { useRouter } from 'vue-router';
export default {
    props: {
        keep: { type: Keep, required: true }
    },
    setup(props) {

        // const router = useRouter();

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