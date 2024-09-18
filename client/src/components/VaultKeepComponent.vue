<template>

    <section class="row d-flex justify-content-center">
        <div @click="getKeepById(keep.id)" class="selectable" type="button" data-bs-toggle="modal"
            data-bs-target="#vaultKeepModal">
            <p class="mb-0 text-center">Views:{{ keep.views }} Kept:{{ keep.kept }}</p>
            <h2 class="text-center">{{ keep.name }}</h2>
            <img :src="keep.img" :alt="keep.name" class="img">
        </div>
        <div v-if="keep.creatorId == account.id">

            <button @click="deleteVaultKeep(vaultKeepId)" type="button" class="btn btn-primary">DELETE</button>
        </div>

    </section>


    <!-- <form action="">
                        <div class="dropdown">
                            <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Dropdown button
                            </button>

                            <ul class="dropdown-menu">
                                <div class="form-select" aria-label="Select a Vault" required>
                                    <li v-for="vault in vaults" :key="vault.id" :value="vault.id" class="dropdown-item"
                                        @click="deleteVaultKeep(vault)">
                                        {{ vault.name }}
                                    </li>
                                </div>
                            </ul>
                        </div>
                    </form> -->

</template>


<script>
import { Keep } from '../models/Keep.js';
import { KeepClone } from '../models/KeepClone.js';
import { keepsService } from '../services/KeepsService.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';
import Pop from '../utils/Pop.js';
import { computed } from 'vue';
import { AppState } from '../AppState';
// import { useRouter } from 'vue-router';
export default {
    props: {
        keep: { type: KeepClone, required: true },
        vaultKeepId: { type: Number }
    },
    setup() {

        // const router = useRouter();

        return {
            // vaultKeeps: computed(() => AppState.vaultKeeps),

            account: computed(() => AppState.account),



            getKeepById(keepId, vaultKeepId) {
                try {
                    keepsService.getKeepById(keepId)
                    // vaultKeepsService.getVaultKeepById(vaultKeepId)
                }
                catch (error) {
                    Pop.error(error);
                }
            },

            async deleteVaultKeep(vaultKeepId) {
                try {
                    const yes = await Pop.confirm()
                    if (!yes) return

                    await vaultKeepsService.deleteVaultKeep(vaultKeepId)
                    // router.push({ name: 'Home' })
                }
                catch (error) {
                    Pop.error(error);
                }
            },

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