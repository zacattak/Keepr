import { Keep } from './Keep.js'

export class KeepClone extends Keep {
    constructor(data) {
        // this.id = data.id
        // this.creatorId = data.creatorId
        super(data)
        this.keepId = data.keepId
        this.vaultId = data.vaultId
        this.vaultKeepId = data.vaultKeepId
    }

}